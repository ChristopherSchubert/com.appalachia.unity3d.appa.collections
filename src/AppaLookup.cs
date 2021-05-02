#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using collections.src.Exceptions;
using collections.src.Interfaces;
using collections.src.Special;
using Unity.Profiling;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace collections.src
{
    [Serializable]
    public abstract class AppaLookup<TKey, TValue, TKeyList, TValueList> : ISerializationCallbackReceiver,
                                                                            IAppaLookupState<TKey, TValue>,
                                                                            IAppaLookup<TKey, TValue, TValueList>,
                                                                            IAppaLookupSafeUpdates<TKey, TValue, TValueList>,
                                                                            IDictionary<TKey, TValue>,
                                                                            IReadOnlyDictionary<TKey, TValue>//,
                                                                            //IReadOnlyList<TValue>
        where TKeyList : AppaList<TKey>, new()
        where TValueList : AppaList<TValue>, new()
    {
        private const string _PRF_PFX = nameof(AppaLookup<TKey, TValue, TKeyList, TValueList>) + ".";

        private const int _REMOVE_INDICES_CAPACITY = 32;

        [SerializeField]
        [ShowIf(nameof(_showDebug))]
        protected int initializerCount = 64;

        [SerializeField]
        [ShowIf(nameof(_showDebug))]
        private TValueList values;

        [SerializeField]
        [ShowIf(nameof(_showDebug))]
        private TKeyList keys;

        [NonSerialized]
        [ShowInInspector]
        [ShowIf(nameof(_showDebug))]
        private Dictionary<TKey, TValue> _lookup;

        [NonSerialized]
        [ShowInInspector]
        [ShowIf(nameof(_showDebug))]
        private Dictionary<TKey, int> _indices;

        [NonSerialized]
        [ShowInInspector]
        [ShowIf(nameof(_showDebug))]
        private bool _isValueUnityObjectChecked;

        [NonSerialized]
        [ShowInInspector]
        [ShowIf(nameof(_showDebug))]
        private bool isValueUnityObject;

        [NonSerialized]
        [ShowInInspector]
        [ShowIf(nameof(_showDebug))]
        private AppaList<int> _tempRemovedIndices;

        [NonSerialized]
        [ShowInInspector]
        [ShowIf(nameof(_showDebug))]
        private Action _setDirtyAction;

        protected virtual bool ReplacesKeysAtStart { get; } = false;

        private static bool _showDebug =>
#if UNITY_EDITOR
            IndexedCollectionsGlobals._UI_DEBUG.Value;
#else
            false;
#endif

        protected virtual void CheckReplacementAtStart(TKey key, TValue value, out TKey newKey, out TValue newValue)
        {
            newKey = key;
            newValue = value;
        }

        protected virtual bool ShouldDisplayTitle { get; } = false;
        protected abstract string GetDisplayTitle(TKey key, TValue value);
        protected abstract string GetDisplaySubtitle(TKey key, TValue value);

        // ReSharper disable once UnusedParameter.Global
        protected abstract Color GetDisplayColor(TKey key, TValue value);

        protected virtual bool AlwaysRefreshDisplayData { get; } = false;
        protected virtual bool NoTracking { get; } = false;

        protected AppaLookup()
        {
            INTERNAL_INITIALIZE(false);
        }

        protected AppaLookup(int capacity)
        {
            initializerCount = capacity;
            INTERNAL_INITIALIZE(false);
        }

        private static readonly ProfilerMarker _PRF_Remove = new ProfilerMarker(_PRF_PFX + nameof(Remove));

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            using (_PRF_Remove.Auto())
            {
                INTERNAL_INITIALIZE();

                var x = RemoveByKey(item.Key);

                return x != null;
            }
        }

        public bool IsReadOnly => false;

        private static readonly ProfilerMarker _PRF_Add = new ProfilerMarker(_PRF_PFX + nameof(Add));

        public void Add(TKey key, TValue value)
        {
            using (_PRF_Add.Auto())
            {
                INTERNAL_INITIALIZE();
                INTERNAL_ADD(key, value);
            }
        }

        public bool Remove(TKey key)
        {
            using (_PRF_Remove.Auto())
            {
                INTERNAL_INITIALIZE();

                if (INTERNAL_CONTAINS(key))
                {
                    var targetIndex = _indices[key];

                    INTERNAL_REMOVE(targetIndex);
                    return true;
                }

                return false;
            }
        }

        private static readonly ProfilerMarker _PRF_CopyTo = new ProfilerMarker(_PRF_PFX + nameof(CopyTo));

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            using (_PRF_CopyTo.Auto())
            {
                INTERNAL_INITIALIZE();

                for (var i = arrayIndex; i < array.Length; i++)
                {
                    array[i] = new KeyValuePair<TKey, TValue>(keys[i - arrayIndex], values[i - arrayIndex]);
                }
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            using (_PRF_Add.Auto())
            {
                INTERNAL_INITIALIZE();
                INTERNAL_ADD(item.Key, item.Value);
            }
        }

        private static readonly ProfilerMarker _PRF_Contains = new ProfilerMarker(_PRF_PFX + nameof(Contains));

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            using (_PRF_Contains.Auto())
            {
                INTERNAL_INITIALIZE();
                return INTERNAL_CONTAINS(item.Key) && INTERNAL_GET(item.Key).Equals(item.Value);
            }
        }

        public ICollection<TValue> Values => values;

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => keys;

        private static readonly ProfilerMarker _PRF_TryGetValue = new ProfilerMarker(_PRF_PFX + nameof(TryGetValue));

        public bool TryGetValue(TKey key, out TValue value)
        {
            using (_PRF_TryGetValue.Auto())
            {
                INTERNAL_INITIALIZE();

                return INTERNAL_TRY_GET(key, out value);
            }
        }

        private static readonly ProfilerMarker _PRF_TryGetValueWithFallback = new ProfilerMarker(_PRF_PFX + nameof(TryGetValueWithFallback));

        public bool TryGetValueWithFallback(
            TKey key,
            out TValue value,
            Predicate<TValue> fallbackCheck,
            string logFallbackAttempt = null,
            string logFallbackFailure = null)
        {
            using (_PRF_TryGetValueWithFallback.Auto())
            {

                if (TryGetValue(key, out value))
                {
                    return true;
                }

                if (logFallbackAttempt != null)
                {
                    Debug.LogWarning(logFallbackAttempt);
                }

                value = FirstWithPreference_NoAlloc(fallbackCheck, out var foundFallback);

                if (foundFallback)
                {
                    return true;
                }

                if (logFallbackFailure != null)
                {
                    Debug.LogWarning(logFallbackFailure);
                }

                value = default;
                return false;
            }
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            INTERNAL_INITIALIZE();
            return _lookup.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            INTERNAL_INITIALIZE();
            return values.GetEnumerator();
        }

        private static readonly ProfilerMarker _PRF_Count = new ProfilerMarker(_PRF_PFX + nameof(Count));

        public int Count
        {
            get
            {
                using (_PRF_Count.Auto())
                {
                    INTERNAL_INITIALIZE();

                    if (keys.Count != values.Count)
                    {
                        throw new KeyNotFoundException($"Key Count: [{keys.Count}] Value Count: [{values.Count}]");
                    }

                    return values.Count;
                }
            }
        }

        private static readonly ProfilerMarker _PRF_Indexer = new ProfilerMarker(_PRF_PFX + "Indexer");

        public TValue this[TKey key]
        {
            get
            {
                using (_PRF_Indexer.Auto())
                {
                    INTERNAL_INITIALIZE();
                    return INTERNAL_GET(key);
                }
            }
            set
            {
                using (_PRF_Indexer.Auto())
                {
                    INTERNAL_INITIALIZE();
                    INTERNAL_UPDATE(key, value);
                }
            }
        }

        private static readonly ProfilerMarker _PRF_ContainsKey = new ProfilerMarker(_PRF_PFX + nameof(ContainsKey));

        public bool ContainsKey(TKey key)
        {
            using (_PRF_ContainsKey.Auto())
            {
                INTERNAL_INITIALIZE();
                return INTERNAL_CONTAINS(key);
            }
        }

        public TValueList at
        {
            get => values;
            set => values = value;
        }
        public TKeyList keysAt
        {
            get => keys;
            set => keys = value;
        }

        /*
        public TValue this[int index]
        {
            get
            {
                using (_PRF_Indexer.Auto())
                {
                    INTERNAL_INITIALIZE();
                    return INTERNAL_GET_VALUE_BY_INDEX(index);
                }
            }
            set
            {
                using (_PRF_Indexer.Auto())
                {
                    INTERNAL_INITIALIZE();
                    INTERNAL_UPDATE_BY_INDEX(index, value);
                }
            }
        }
        */

        private static readonly ProfilerMarker _PRF_AddIfKeyNotPresent = new ProfilerMarker(_PRF_PFX + nameof(AddIfKeyNotPresent));

        public void AddIfKeyNotPresent(TKey key, Func<TValue> value)
        {
            using (_PRF_AddIfKeyNotPresent.Auto())
            {
                INTERNAL_INITIALIZE();

                if (!INTERNAL_CONTAINS(key))
                {
                    INTERNAL_ADD(key, value());
                }
            }
        }

        public void AddIfKeyNotPresent(TKey key, TValue value)
        {
            using (_PRF_AddIfKeyNotPresent.Auto())
            {
                INTERNAL_INITIALIZE();

                if (!INTERNAL_CONTAINS(key))
                {
                    INTERNAL_ADD(key, value);
                }
            }
        }

        private static readonly ProfilerMarker _PRF_AddOrUpdateIf = new ProfilerMarker(_PRF_PFX + nameof(AddOrUpdateIf));

        public void AddOrUpdateIf(TKey key, Func<TValue> valueRetriever, Predicate<TValue> updateIf)
        {
            using (_PRF_AddOrUpdateIf.Auto())
            {
                INTERNAL_INITIALIZE();

                if (!INTERNAL_CONTAINS(key))
                {
                    INTERNAL_ADD(key, valueRetriever());
                    return;
                }

                var v = _lookup[key];

                if (updateIf(v))
                {
                    INTERNAL_UPDATE(key, valueRetriever());
                }
            }
        }

        public void AddOrUpdateIf(TKey key, TValue value, Predicate<TValue> updateIf)
        {
            using (_PRF_AddOrUpdateIf.Auto())
            {
                INTERNAL_INITIALIZE();

                if (!INTERNAL_CONTAINS(key))
                {
                    INTERNAL_ADD(key, value);
                    return;
                }

                var v = _lookup[key];

                if (updateIf(v))
                {
                    INTERNAL_UPDATE(key, value);
                }
            }
        }

        private static readonly ProfilerMarker _PRF_AddOrUpdate = new ProfilerMarker(_PRF_PFX + nameof(AddOrUpdate));

        public void AddOrUpdate(TKey key, TValue value)
        {
            using (_PRF_AddOrUpdate.Auto())
            {
                INTERNAL_INITIALIZE();

                if (!INTERNAL_CONTAINS(key))
                {
                    INTERNAL_ADD(key, value);
                }
                else
                {
                    INTERNAL_UPDATE(key, value);
                }
            }
        }

        public void AddOrUpdate(TKey key, Func<TValue> add, Func<TValue> update)
        {
            using (_PRF_AddOrUpdate.Auto())
            {
                INTERNAL_INITIALIZE();

                if (!INTERNAL_CONTAINS(key))
                {
                    INTERNAL_ADD(key, add());
                }
                else
                {
                    INTERNAL_UPDATE(key, update());
                }
            }
        }

        public void AddOrUpdate(KeyValuePair<TKey, TValue> pair)
        {
            using (_PRF_AddOrUpdate.Auto())
            {
                AddOrUpdate(pair.Key, pair.Value);
            }
        }

        private static readonly ProfilerMarker _PRF_RemoveByKey = new ProfilerMarker(_PRF_PFX + nameof(RemoveByKey));

        public TValue RemoveByKey(TKey key)
        {
            using (_PRF_RemoveByKey.Auto())
            {
                INTERNAL_INITIALIZE();

                if (!INTERNAL_CONTAINS(key))
                {
                    return default;
                }

                var targetIndex = _indices[key];

                return INTERNAL_REMOVE(targetIndex);
            }
        }

        private static readonly ProfilerMarker _PRF_RemoveAt = new ProfilerMarker(_PRF_PFX + nameof(RemoveAt));

        public TValue RemoveAt(int targetIndex)
        {
            using (_PRF_RemoveAt.Auto())
            {
                INTERNAL_INITIALIZE();

                return INTERNAL_REMOVE(targetIndex);
            }
        }

        public TValue Remove(KeyValuePair<TKey, TValue> pair)
        {
            using (_PRF_Remove.Auto())
            {
                return RemoveByKey(pair.Key);
            }
        }

        private static readonly ProfilerMarker _PRF_AddOrUpdateRange = new ProfilerMarker(_PRF_PFX + nameof(AddOrUpdateRange));

        public void AddOrUpdateRange(IList<TValue> vals, Func<TValue, TKey> selector)
        {
            using (_PRF_AddOrUpdateRange.Auto())
            {
                INTERNAL_INITIALIZE();

                for (var i = 0; i < vals.Count; i++)
                {
                    var value = INTERNAL_GET_VALUE_BY_INDEX(i);

                    var key = selector(value);

                    if (!INTERNAL_CONTAINS(key))
                    {
                        INTERNAL_ADD(key, value);
                    }
                    else
                    {
                        INTERNAL_UPDATE(key, value);
                    }
                }
            }
        }

        private static readonly ProfilerMarker _PRF_Clear = new ProfilerMarker(_PRF_PFX + nameof(Clear));

        public void Clear()
        {
            using (_PRF_Clear.Auto())
            {
                INTERNAL_INITIALIZE();
                INTERNAL_CLEAR();
            }
        }

        private static readonly ProfilerMarker _PRF_GetKeyByIndex = new ProfilerMarker(_PRF_PFX + nameof(GetKeyByIndex));

        public TKey GetKeyByIndex(int i)
        {
            using (_PRF_GetKeyByIndex.Auto())
            {
                INTERNAL_INITIALIZE();

                return INTERNAL_GET_KEY_BY_INDEX(i);
            }
        }

        private static readonly ProfilerMarker _PRF_GetByIndex = new ProfilerMarker(_PRF_PFX + nameof(GetByIndex));

        public TValue GetByIndex(int i)
        {
            using (_PRF_GetByIndex.Auto())
            {
                INTERNAL_INITIALIZE();

                return INTERNAL_GET_VALUE_BY_INDEX(i);
            }
        }

        private static readonly ProfilerMarker _PRF_Get = new ProfilerMarker(_PRF_PFX + nameof(Get));

        public TValue Get(TKey key)
        {
            using (_PRF_Get.Auto())
            {
                INTERNAL_INITIALIZE();

                if (INTERNAL_CONTAINS(key))
                {
                    return INTERNAL_GET(key);
                }

                throw new KeyNotFoundException($"Could not find key [{key}] in collection.");
            }
        }

        private static readonly ProfilerMarker _PRF_TryGet = new ProfilerMarker(_PRF_PFX + nameof(TryGet));

        public bool TryGet(TKey key, out TValue value)
        {
            using (_PRF_TryGet.Auto())
            {
                INTERNAL_INITIALIZE();
                return INTERNAL_TRY_GET(key, out value);
            }
        }

        private static readonly ProfilerMarker _PRF_IfPresent = new ProfilerMarker(_PRF_PFX + nameof(IfPresent));

        public void IfPresent(TKey key, Action present, Action notPresent)
        {
            using (_PRF_IfPresent.Auto())
            {
                INTERNAL_INITIALIZE();

                if (INTERNAL_CONTAINS(key))
                {
                    present();
                }
                else
                {
                    notPresent();
                }
            }
        }

        private static readonly ProfilerMarker _PRF_SumCounts = new ProfilerMarker(_PRF_PFX + nameof(SumCounts));

        public int SumCounts(Func<TValue, int> counter)
        {
            using (_PRF_SumCounts.Auto())
            {
                INTERNAL_INITIALIZE();

                var sum = 0;

                var count = Count;

                for (var i = 0; i < count; i++)
                {
                    sum += counter(INTERNAL_GET_VALUE_BY_INDEX(i));
                }

                return sum;
            }
        }

        [field: NonSerialized] public int LastFrameCheck { get; private set; }

        public int InitializerCount
        {
            get => initializerCount;
            set => initializerCount = value;
        }

        public void SetDirtyAction(Action a)
        {
            _setDirtyAction = a;
        }

        public IReadOnlyList<TKey> Keys => keys;

        IReadOnlyList<TValue> IIndexedCollectionState<TValue>.Values => values;

        public IReadOnlyDictionary<TKey, TValue> Lookup => _lookup;

        public IReadOnlyDictionary<TKey, int> Indices => _indices;

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                INTERNAL_INITIALIZE();
                return keys;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                INTERNAL_INITIALIZE();
                return values;
            }
        }


        private static readonly ProfilerMarker _PRF_OnBeforeSerialize =
            new ProfilerMarker(_PRF_PFX + nameof(ISerializationCallbackReceiver.OnBeforeSerialize));

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            using (_PRF_OnBeforeSerialize.Auto())
            {
                keys.TrimExcess();
                values.TrimExcess();
            }
        }

        private static readonly ProfilerMarker _PRF_OnAfterDeserialize =
            new ProfilerMarker(_PRF_PFX + nameof(ISerializationCallbackReceiver.OnAfterDeserialize));

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            using (_PRF_OnAfterDeserialize.Auto())
            {
                if (ReplacesKeysAtStart)
                {
                    for (var i = 0; i < keys.Count; i++)
                    {
                        CheckReplacementAtStart(keys[i], values[i], out var newKey, out var newValue);
                        keys[i] = newKey;
                        values[i] = newValue;
                    }
                }

                INTERNAL_INITIALIZE(false);
            }
        }

        private static readonly ProfilerMarker _PRF_RemoveNulls = new ProfilerMarker(_PRF_PFX + nameof(RemoveNulls));

        public void RemoveNulls()
        {
            using (_PRF_RemoveNulls.Auto())
            {
                var rebuild = false;

                if (_tempRemovedIndices == null)
                {
                    _tempRemovedIndices = new NonSerializedList<int>(_REMOVE_INDICES_CAPACITY, noTracking: true);
                }

                _tempRemovedIndices.ClearFast();

                if (values.RemoveNulls(_tempRemovedIndices))
                {
                    for (var i = 0; i < _tempRemovedIndices.Count; i++)
                    {
                        keys.RemoveAt(i);
                    }

                    rebuild = true;
                }

                if (rebuild)
                {
                    INTERNAL_REBUILD();
                }

                _tempRemovedIndices.ClearFast();
            }
        }

        private static readonly ProfilerMarker _PRF_Any = new ProfilerMarker(_PRF_PFX + nameof(Any));

        public bool Any(Predicate<TValue> check)
        {
            using (_PRF_Any.Auto())
            {
                for (var i = 0; i < values.Count; i++)
                {
                    if (check(values[i]))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        private static readonly ProfilerMarker _PRF_All = new ProfilerMarker(_PRF_PFX + nameof(All));
        private static readonly ProfilerMarker _PRF_First = new ProfilerMarker(_PRF_PFX + nameof(First_NoAlloc));
        private static readonly ProfilerMarker _PRF_FirstOrDefault = new ProfilerMarker(_PRF_PFX + nameof(FirstOrDefault_NoAlloc));

        public bool All(Predicate<TValue> check)
        {
            using (_PRF_All.Auto())
            {
                for (var i = 0; i < values.Count; i++)
                {
                    if (!check(values[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        
        public int CountKeys_NoAlloc(Predicate<TKey> pred)
        {
            return keys.Count_NoAlloc(pred);
        }

        public int CountValues_NoAlloc(Predicate<TValue> pred)
        {
            return values.Count_NoAlloc(pred);
        }

        
        public TValue First_NoAlloc(Predicate<TValue> check)
        {
            using (_PRF_First.Auto())
            {
                for (var i = 0; i < values.Count; i++)
                {
                    if (check(values[i]))
                    {
                        return values[i];
                    }
                }

                throw new InvalidOperationException("Not found.");
            }
        }

        public TValue FirstOrDefault_NoAlloc(Predicate<TValue> check)
        {
            using (_PRF_FirstOrDefault.Auto())
            {
                for (var i = 0; i < values.Count; i++)
                {
                    if (check(values[i]))
                    {
                        return values[i];
                    }
                }

                return default;
            }
        }

        private static readonly ProfilerMarker _PRF_FirstWithPreference_NoAlloc = new ProfilerMarker(_PRF_PFX + nameof(FirstWithPreference_NoAlloc));
        public TValue FirstWithPreference_NoAlloc(Predicate<TValue> preferred, out bool foundAny)
        {
            using (_PRF_FirstWithPreference_NoAlloc.Auto())
            {
                TValue found = default;
                foundAny = false;
                
                for (var i = 0; i < values.Count; i++)
                {                    
                    var checking = values[i];
                    
                    if (preferred(checking))
                    {
                        foundAny = true;
                        return checking;
                    }
                    
                    if (i == 0)
                    {
                        foundAny = true;
                        found = checking;
                    }
                }

                return found;
            }
        }

        public TValue FirstWithPreference_NoAlloc(Predicate<TValue> preferred, Predicate<TValue> required, out bool foundAny)
        {
            using (_PRF_FirstWithPreference_NoAlloc.Auto())
            {
                TValue found = default;
                var foundRequired = false;
                foundAny = false;
                
                for (var i = 0; i < values.Count; i++)
                {
                    var checking = values[i];

                    if (preferred(checking))
                    {
                    foundAny = true;
                        return checking;
                    }
                    
                    if (!foundRequired && required(checking))
                    {
                    foundAny = true;
                        found = checking;
                        foundRequired = true;
                    }
                }

                return found;
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_CLEAR = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_CLEAR));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void INTERNAL_CLEAR()
        {
            using (_PRF_INTERNAL_CLEAR.Auto())
            {
                keys.Clear();
                values.Clear();
                _indices.Clear();
                _lookup.Clear();
                _setDirtyAction?.Invoke();
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_TRY_GET = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_TRY_GET));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool INTERNAL_TRY_GET(TKey key, out TValue value)
        {
            using (_PRF_INTERNAL_TRY_GET.Auto())
            {
                return _lookup.TryGetValue(key, out value);
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_GET = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_GET));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TValue INTERNAL_GET(TKey key)
        {
            using (_PRF_INTERNAL_GET.Auto())
            {
                if (INTERNAL_CONTAINS(key))
                {
                    return _lookup[key];
                }

                throw new KeyNotFoundException($"Could not find key [{key}] in collection.");
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_GET_KEY_BY_INDEX = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_GET_KEY_BY_INDEX));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TKey INTERNAL_GET_KEY_BY_INDEX(int index)
        {
            using (_PRF_INTERNAL_GET_KEY_BY_INDEX.Auto())
            {
                return keys[index];
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_GET_VALUE_BY_INDEX = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_GET_VALUE_BY_INDEX));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TValue INTERNAL_GET_VALUE_BY_INDEX(int index)
        {
            using (_PRF_INTERNAL_GET_VALUE_BY_INDEX.Auto())
            {
                return values[index];
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_CONTAINS = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_CONTAINS));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool INTERNAL_CONTAINS(TKey key)
        {
            using (_PRF_INTERNAL_CONTAINS.Auto())
            {
                return _lookup.ContainsKey(key);
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_REMOVE = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_REMOVE));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TValue INTERNAL_REMOVE(int targetIndex)
        {
            using (_PRF_INTERNAL_REMOVE.Auto())
            {
                if ((targetIndex >= keys.Count) || (targetIndex < 0))
                {
                    throw new IndexOutOfRangeException($"Index [{targetIndex}] is out of range for collection of length [{keys.Count}].");
                }

                var deletingKey = keys[targetIndex];
                var deletingValue = values[targetIndex];

                _lookup.Remove(deletingKey);

                _indices.Remove(deletingKey);

                var lastIndex = keys.Count - 1;

                if (lastIndex != targetIndex)
                {
                    var lastKey = keys[lastIndex];
                    var lastValue = values[lastIndex];

                    keys[targetIndex] = lastKey;
                    values[targetIndex] = lastValue;

                    _indices[lastKey] = targetIndex;
                }

                keys.RemoveAt(lastIndex);
                values.RemoveAt(lastIndex);

                _setDirtyAction?.Invoke();
                return deletingValue;
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_ADD = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_ADD));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void INTERNAL_ADD(TKey key, TValue value)
        {
            using (_PRF_INTERNAL_ADD.Auto())
            {
                _lookup.Add(key, value);

                _indices.Add(key, _indices.Count);
                values.Add(value);
                keys.Add(key);
                _setDirtyAction?.Invoke();
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_UPDATE_BY_INDEX = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_UPDATE_BY_INDEX));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void INTERNAL_UPDATE_BY_INDEX(int index, TValue value)
        {
            using (_PRF_INTERNAL_UPDATE_BY_INDEX.Auto())
            {
                var key = keys[index];
                _lookup[key] = value;
                values[index] = value;
                _setDirtyAction?.Invoke();
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_UPDATE = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_UPDATE));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void INTERNAL_UPDATE(TKey key, TValue value)
        {
            using (_PRF_INTERNAL_UPDATE.Auto())
            {
                var index = _indices[key];
                values[index] = value;

                _lookup[key] = value;

                _setDirtyAction?.Invoke();
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_INITIALIZE = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_INITIALIZE));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void INTERNAL_INITIALIZE(bool getFrameCount = true)
        {
            using (_PRF_INTERNAL_INITIALIZE.Auto())
            {
                var frameCount = -1;

                if (getFrameCount)
                {
                    frameCount = Time.frameCount;
                }

                if (LastFrameCheck == frameCount)
                {
                    return;
                }

                LastFrameCheck = frameCount;

                INTERNAL_INITIALIZE_EXECUTE();
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_INITIALIZE_EXECUTE = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_INITIALIZE_EXECUTE));

        private void INTERNAL_INITIALIZE_EXECUTE()
        {
            using (_PRF_INTERNAL_INITIALIZE_EXECUTE.Auto())
            {
                if (keys == null)
                {
                    keys = new TKeyList {Capacity = initializerCount};
                    _setDirtyAction?.Invoke();
                }

                if (values == null)
                {
                    values = new TValueList {Capacity = initializerCount};
                    _setDirtyAction?.Invoke();
                }

                if (_lookup == null)
                {
                    _lookup = new Dictionary<TKey, TValue>(initializerCount);
                    _setDirtyAction?.Invoke();
                }

                if (_indices == null)
                {
                    _indices = new Dictionary<TKey, int>(initializerCount);
                    _setDirtyAction?.Invoke();
                }

                if (!_isValueUnityObjectChecked)

                {
                    isValueUnityObject = typeof(Object).IsAssignableFrom(typeof(TValue));
                    _isValueUnityObjectChecked = true;
                }

                INTERNAL_CHECK_FATAL();

                if (INTERNAL_REQUIRES_REBUILD())
                {
                    INTERNAL_REBUILD();
                }

                INTERNAL_CHECK_FATAL();

                if (values.Count > initializerCount)
                {
                    initializerCount = values.Count;
                    _setDirtyAction?.Invoke();
                }
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_CHECK_FATAL = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_CHECK_FATAL));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void INTERNAL_CHECK_FATAL()
        {
            using (_PRF_INTERNAL_CHECK_FATAL.Auto())
            {
                var valueCount = values.Count;
                var keyCount = keys.Count;

                var countMismatch = keyCount != valueCount;

                if (countMismatch)
                {
                    throw new IndexedKeyValueMismatchException(keyCount, valueCount);
                }
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_REQUIRES_REBUILD = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_REQUIRES_REBUILD));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool INTERNAL_REQUIRES_REBUILD()
        {
            using (_PRF_INTERNAL_REQUIRES_REBUILD.Auto())
            {
                var valueCount = values.Count;

                if (valueCount == 0)
                {
                    keys.ClearFast();
                    _lookup.Clear();
                    _indices.Clear();
                    _setDirtyAction?.Invoke();
                    return false;
                }

                var mustRebuild = false;

                if (_tempRemovedIndices == null)
                {
                    _tempRemovedIndices = new NonSerializedList<int>(_REMOVE_INDICES_CAPACITY, noTracking: true);
                }

                if (isValueUnityObject)
                {
                    _tempRemovedIndices.ClearFast();

                    if (values.RemoveNulls(_tempRemovedIndices))
                    {
                        for (var i = 0; i < _tempRemovedIndices.Count; i++)
                        {
                            keys.RemoveAt(i);
                        }

                        mustRebuild = true;
                    }
                }

                _tempRemovedIndices.ClearFast();

                if (keys.RemoveNulls(_tempRemovedIndices))
                {
                    for (var i = 0; i < _tempRemovedIndices.Count; i++)
                    {
                        values.RemoveAt(i);
                    }

                    mustRebuild = true;
                }

                if (mustRebuild)
                {
                    return true;
                }

                var lookupCount = _lookup.Count;
                var indexCount = _indices.Count;

                if ((lookupCount != valueCount) || (indexCount != valueCount))
                {
                    return true;
                }

                return false;
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_REBUILD = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_REBUILD));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void INTERNAL_REBUILD()
        {
            using (_PRF_INTERNAL_REBUILD.Auto())
            {
                _lookup.Clear();
                _indices.Clear();

                var newIndex = 0;

                for (var i = 0; i < values.Count; i++)
                {
                    var value = values[i];
                    var key = keys[i];

                    if (key == null)
                    {
                        var message = $"Null key cannot be added.  Index: {i} | Value: {value}";
                        throw new ArgumentException(message);
                    }
                    
                    if (_indices.ContainsKey(key))
                    {
                        var existingIndex = _indices[key];
                        
                        var message = $"The key was already added.  Index: {i} | Existing Index: {existingIndex} | Key: {key}";
                        throw new ArgumentException(message);
                    }

                    _indices.Add(key, newIndex);
                    _lookup.Add(key, value);

                    newIndex += 1;
                }

                _setDirtyAction?.Invoke();
            }
        }
        
        #region Sorting

        private static readonly ProfilerMarker _PRF_Reverse = new ProfilerMarker(_PRF_PFX + nameof(Reverse));
        /// <summary>Reverses the sequence of the elements in the entire index.</summary>
        public void Reverse()
        {
            using (_PRF_Reverse.Auto())
            {
                Reverse(0, Count);
            }
        }

        public void Reverse(int index, int length)
        {
            using (_PRF_Reverse.Auto())
            {
                keys.Reverse(index, length);
                values.Reverse(index, length);
            
                INTERNAL_REBUILD();
            }
        }

        private static readonly ProfilerMarker _PRF_SortByKey = new ProfilerMarker(_PRF_PFX + nameof(SortByKey));
        
        public void SortByKey(IComparer<TKey> comparer)
        {
            using (_PRF_SortByKey.Auto())
            {
                SortByKey(0, Count, comparer);
            }
        }

        public void SortByKey(int index, int length, IComparer<TKey> comparer)
        {
            using (_PRF_SortByKey.Auto())
            {
                keys.Sort(values, index, length, comparer);
            
                INTERNAL_REBUILD();
            }
        }

        public void SortByKey(Comparison<TKey> comparison)
        {
            using (_PRF_SortByKey.Auto())
            {
                keys.Sort(values, comparison);
            
                INTERNAL_REBUILD();
            }
        }

        private static readonly ProfilerMarker _PRF_SortByValue = new ProfilerMarker(_PRF_PFX + nameof(SortByValue));
        
        public void SortByValue(IComparer<TValue> comparer)
        {
            using (_PRF_SortByValue.Auto())
            {
                SortByValue(0, Count, comparer);
            }
        }

        public void SortByValue(int index, int length, IComparer<TValue> comparer)
        {
            using (_PRF_SortByValue.Auto())
            {
                values.Sort(keys, index, length, comparer);
            
                INTERNAL_REBUILD();
            }
        }

        public void SortByValue(Comparison<TValue> comparison)
        {
            using (_PRF_SortByValue.Auto())
            {
                values.Sort(keys, comparison);

                INTERNAL_REBUILD();
            }
        }

        
        #endregion
        
        #region Display
        

        private static readonly ProfilerMarker _PRF_Insert = new ProfilerMarker(_PRF_PFX + nameof(Insert));

        internal void Insert(int index, TKey key, TValue value)
        {
            using (_PRF_Insert.Auto())
            {
                INTERNAL_INITIALIZE();

                if (Equals(key, keys[index]))
                {
                    return;
                }

                keys.Insert(index, key);
                values.Insert(index, value);

                INTERNAL_REBUILD();
            }
        }

        private static readonly ProfilerMarker _PRF_GetKeyValuePair = new ProfilerMarker(_PRF_PFX + nameof(GetKeyValuePair));

        private NonSerializedList<KVPDisplayWrapper> _displayWrappers;
        
        internal KVPDisplayWrapper GetKeyValuePair(int index)
        {
            using (_PRF_GetKeyValuePair.Auto())
            {
                if (_displayWrappers == null)
                {
                    _displayWrappers = new NonSerializedList<KVPDisplayWrapper>(Count);
                }

                _displayWrappers.Count = Count;
                
                var wrapper = _displayWrappers[index];

                INTERNAL_INITIALIZE();
                var key = keys[index];
                var value = values[index];
                
                if (wrapper == null)
                {
                    wrapper = new KVPDisplayWrapper(
                        ShouldDisplayTitle,
                        GetDisplayTitle(key, value),
                        GetDisplaySubtitle(key, value),
                        GetDisplayColor(key, value),
                        key,
                        value,
                        INTERNAL_UPDATE
                    );
                }
                else
                {
                    if (AlwaysRefreshDisplayData || !wrapper.Key.Equals(key))
                    {
                        wrapper.Update(ShouldDisplayTitle,
                            GetDisplayTitle(key, value),
                            GetDisplaySubtitle(key, value),
                            GetDisplayColor(key, value),
                            key,
                            value,
                            INTERNAL_UPDATE);                        
                    }
                }

                _displayWrappers[index] = wrapper;

                return wrapper;
            }
        }

        [HideReferenceObjectPicker]
        public class KVPDisplayWrapper : IEquatable<KVPDisplayWrapper>
        {
            private const string _PRF_PFX = nameof(AppaLookup<TKey,TValue,TKeyList,TValueList>.KVPDisplayWrapper) + ".";
            private static readonly ProfilerMarker _PRF_KVPDisplayWrapper = new ProfilerMarker(_PRF_PFX + nameof(KVPDisplayWrapper));
            public KVPDisplayWrapper(
                bool showTitle,
                string title,
                string subtitle,
                Color color,
                TKey key,
                TValue value,
                Action<TKey, TValue> updateAction)
            {
                using (_PRF_KVPDisplayWrapper.Auto())
                {
                    _disableTitle = !showTitle;
                    _title = title;
                    _subtitle = subtitle;
                    _color = color;
                    _pair = new KeyValuePair<TKey, TValue>(key, value);
                    _updateAction = updateAction;
                }
            }

            private Action<TKey, TValue> _updateAction;
            private bool _disableTitle;
            private string _title;
            private string _subtitle;
            private Color _color;
            private KeyValuePair<TKey, TValue> _pair;

            private const string _titlePointer = "$" + nameof(_title);
            private const string _subtitlePointer = "$" + nameof(_subtitle);
            private const string _disableTitlePointer = nameof(_disableTitle);
            private const string _colorPointer = nameof(_color);
            
            //[ShowInInspector]
            [ReadOnly] public TKey Key => _pair.Key;

            private static readonly ProfilerMarker _PRF_Value = new ProfilerMarker(_PRF_PFX + nameof(Value));
            [ShowInInspector]
            [InlineProperty]
            [HideLabel]
            [LabelWidth(0)]
            [SmartTitle(_titlePointer, _subtitlePointer, 
                TitleAlignments.Split,
                hideIfMemberName: _disableTitlePointer, color: _colorPointer
                
            )]
            public TValue Value
            {
                get => _pair.Value;
                set
                {
                    using (_PRF_Value.Auto())
                    {
                        _updateAction?.Invoke(Key, value);
                        _pair = new KeyValuePair<TKey, TValue>(Key, value);
                    }
                }
            }

            private static readonly ProfilerMarker _PRF_Update = new ProfilerMarker(_PRF_PFX + nameof(Update));
            public void Update(
                bool showTitle,
                string title,
                string subtitle,
                Color color,
                TKey key,
                TValue value,
                Action<TKey, TValue> updateAction)
            {
                using (_PRF_Update.Auto())
                {
                    _disableTitle = !showTitle;
                    _title = title;
                    _subtitle = subtitle;
                    _color = color;
                    _pair = new KeyValuePair<TKey, TValue>(key, value);
                    _updateAction = updateAction;
                }
            }
            
            #region IEquatable

            public bool Equals(KVPDisplayWrapper other)
            {
                if (ReferenceEquals(null, other))
                {
                    return false;
                }

                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                return Equals(_updateAction, other._updateAction) && _disableTitle == other._disableTitle && _title == other._title && _subtitle == other._subtitle && _color.Equals(other._color) && _pair.Equals(other._pair);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != this.GetType())
                {
                    return false;
                }

                return Equals((KVPDisplayWrapper) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (_updateAction != null ? _updateAction.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ _disableTitle.GetHashCode();
                    hashCode = (hashCode * 397) ^ (_title != null ? _title.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (_subtitle != null ? _subtitle.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ _color.GetHashCode();
                    hashCode = (hashCode * 397) ^ _pair.GetHashCode();
                    return hashCode;
                }
            }

            public static bool operator ==(KVPDisplayWrapper left, KVPDisplayWrapper right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(KVPDisplayWrapper left, KVPDisplayWrapper right)
            {
                return !Equals(left, right);
            }

#endregion
        }
        
        #endregion
    }
}
