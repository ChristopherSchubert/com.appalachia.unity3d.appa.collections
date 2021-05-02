using System;
using Unity.Profiling;
using UnityEngine;

namespace collections.src
{
    [Serializable]
    public abstract class AppaLookup2<TKey1, TKey2, TValue, TKey1List, TKey2List, TValueList, TNested, TNestedList> :
        AppaLookup<TKey1, TNested, TKey1List, TNestedList>
        where TKey1List : AppaList<TKey1>, new()
        where TKey2List : AppaList<TKey2>, new()
        where TValueList : AppaList<TValue>, new()
        where TNested : AppaLookup<TKey2, TValue, TKey2List, TValueList>, new()
        where TNestedList : AppaList<TNested>, new()
    {

        private const string _PRF_PFX = nameof(AppaLookup2<TKey1,TKey2,TValue,TKey1List,TKey2List,TValueList,TNested,TNestedList>) + ".";

        private static readonly ProfilerMarker _PRF_AddOrUpdate = new ProfilerMarker(_PRF_PFX + nameof(AddOrUpdate));
        public void AddOrUpdate(TKey1 primary, TKey2 secondary, TValue value)
        {
            using (_PRF_AddOrUpdate.Auto())
            {
                if (!TryGetValue(primary, out var sub))
                {
                    sub = new TNested();
                
                    Add(primary, sub);;
                }

                sub.AddOrUpdate(secondary, value);
            }
        }
        
        
        private static readonly ProfilerMarker _PRF_TryGetValue = new ProfilerMarker(_PRF_PFX + nameof(TryGetValue));

        public bool TryGetValue(TKey1 primary, TKey2 secondary, out TValue value)
        {
            using (_PRF_TryGetValue.Auto())
            {
                if (base.TryGetValue(primary, out var sub1))
                {
                    if (sub1.TryGetValue(secondary, out value))
                    {
                        return true;
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
            out TValue value,
            Predicate<TValue> fallbackCheck,
            string logFallbackAttempt = null,
            string logFallbackFailure = null)
        {
            using (_PRF_TryGetValueWithFallback.Auto())
            {
                if (base.TryGetValue(primary, out var sub1))
                {            
                    if (sub1.TryGet(secondary, out value))
                    {
                        return true;
                    }

                    if (logFallbackAttempt != null)
                    {
                        Debug.LogWarning(logFallbackAttempt);
                    }
                    
                    value = sub1.FirstWithPreference_NoAlloc(fallbackCheck, out var foundFallback);

                    if (foundFallback)
                    {
                        return true;
                    }

                    if (logFallbackFailure != null)
                    {
                        Debug.LogWarning(logFallbackFailure);
                    }                       
                    
                }

                value = default;
                return false;
            }
        }

        public bool ContainsKeys(TKey1 key1, TKey2 key2)
        {
            if (!ContainsKey(key1))
            {
                return false;
            }

            return this[key1].ContainsKey(key2);
        }
    }
}