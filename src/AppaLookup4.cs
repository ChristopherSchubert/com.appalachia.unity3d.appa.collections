#region

using System;
using Unity.Profiling;
using UnityEngine;

#endregion

namespace collections.src
{
    [Serializable]
    public abstract class AppaLookup4<TKey1, TKey2, TKey3, TKey4, TValue, TKey1List, TKey2List, TKey3List, TKey4List, TValueList,
                                               TSubSubNested, TSubNested, TNested, TSubSubNestedList, TSubNestedList,
                                               TNestedList> : AppaLookup<TKey1, TNested, TKey1List, TNestedList>
        where TKey1List : AppaList<TKey1>, new()
        where TKey2List : AppaList<TKey2>, new()
        where TKey3List : AppaList<TKey3>, new()
        where TKey4List : AppaList<TKey4>, new()
        where TValueList : AppaList<TValue>, new()
        where TSubSubNested : AppaLookup<TKey4, TValue, TKey4List, TValueList>, new()
        where TSubNested : AppaLookup2<TKey3, TKey4, TValue, TKey3List, TKey4List, TValueList, TSubSubNested, TSubSubNestedList>, new()
        where TNested : AppaLookup3<TKey2, TKey3, TKey4, TValue, TKey2List, TKey3List, TKey4List, TValueList, TSubSubNested, TSubNested,
            TSubSubNestedList, TSubNestedList>, new()
        where TSubSubNestedList : AppaList<TSubSubNested>, new()
        where TSubNestedList : AppaList<TSubNested>, new()
        where TNestedList : AppaList<TNested>, new()

    {
        private const string _PRF_PFX =
            nameof(AppaLookup4<TKey1, TKey2, TKey3, TKey4, TValue, TKey1List, TKey2List, TKey3List, TKey4List, TValueList, TSubSubNested,
                TSubNested, TNested, TSubSubNestedList, TSubNestedList, TNestedList>) +
            ".";

        private static readonly ProfilerMarker _PRF_AddOrUpdate = new ProfilerMarker(_PRF_PFX + nameof(AddOrUpdate));

        public void AddOrUpdate(TKey1 primary, TKey2 secondary, TKey3 tertiary, TKey4 quaternary, TValue value)
        {
            using (_PRF_AddOrUpdate.Auto())
            {
                if (!TryGetValue(primary, out var sub))
                {
                    sub = new TNested();

                    Add(primary, sub);
                }

                if (!sub.TryGetValue(secondary, out var subSub))
                {
                    subSub = new TSubNested();

                    sub.Add(secondary, subSub);
                }

                if (!subSub.TryGetValue(tertiary, out var subSubSub))
                {
                    subSubSub = new TSubSubNested();

                    subSub.Add(tertiary, subSubSub);
                }

                subSubSub.AddOrUpdate(quaternary, value);
            }
        }

        private static readonly ProfilerMarker _PRF_TryGetValue = new ProfilerMarker(_PRF_PFX + nameof(TryGetValue));

        public bool TryGetValue(TKey1 primary, TKey2 secondary, TKey3 tertiary, TKey4 quaternary, out TValue value)
        {
            using (_PRF_TryGetValue.Auto())
            {
                if (base.TryGetValue(primary, out var sub1))
                {
                    if (sub1.TryGetValue(secondary, out var sub2))
                    {
                        if (sub2.TryGetValue(tertiary, out var sub3))
                        {
                            if (sub3.TryGet(quaternary, out value))
                            {
                                return true;
                            }                          
                            
                        }
                    }
                }

                value = default;
                return false;
            }
        }

        private static readonly ProfilerMarker _PRF_TryGetValueWithFallback = new ProfilerMarker(_PRF_PFX + nameof(TryGetValueWithFallback));

        public bool TryGetValueWithFallback(
            TKey1 primary,
            TKey2 secondary,
            TKey3 tertiary,
            TKey4 quaternary,
            out TValue value,
            Predicate<TValue> fallbackCheck,
            string logFallbackAttempt = null,
            string logFallbackFailure = null)
        {
            using (_PRF_TryGetValueWithFallback.Auto())
            {
                if (base.TryGetValue(primary, out var sub1))
                {
                    if (sub1.TryGetValue(secondary, out var sub2))
                    {
                        if (sub2.TryGetValue(tertiary, out var sub3))
                        {
                            if (sub3.TryGet(quaternary, out value))
                            {
                                return true;
                            }

                            if (logFallbackAttempt != null)
                            {
                                Debug.LogWarning(logFallbackAttempt);
                            }
                            
                            value = sub3.FirstWithPreference_NoAlloc(fallbackCheck, out var foundFallback);

                            if (foundFallback)
                            {
                                return true;
                            }

                            if (logFallbackFailure != null)
                            {
                                Debug.LogWarning(logFallbackFailure);
                            }
                        }
                    }
                }

                value = default;
                return false;
            }
        }
    }
}
