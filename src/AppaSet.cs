#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using collections.src.Interfaces;
using Unity.Profiling;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace collections.src
{
    [Serializable]
    public abstract class AppaSet<TValue, TValueList> : ISerializationCallbackReceiver,
                                                           IAppaSetState<TValue>,
                                                           IAppaSet<TValue>,
                                                           ICollection<TValue>,
                                                           IReadOnlyList<TValue>
        where TValueList : AppaList<TValue>, new()
    {
        private const string _PRF_PFX = nameof(AppaSet<TValue, TValueList>) + ".";

        [SerializeField, HideInInspector]
        private int initializerCount = 64;

        [SerializeField, HideInInspector]
        private TValueList values;

        [NonSerialized] private Dictionary<TValue, int> _indices;

        [NonSerialized] private bool _isValueUnityObjectChecked;

        [NonSerialized] private bool isValueUnityObject;

        [NonSerialized] private Action _setDirtyAction;

        protected AppaSet()
        {
            INTERNAL_INITIALIZE(false);
        }

        public bool NoTracking { get; set; } = false;

        private static readonly ProfilerMarker _PRF_Remove = new ProfilerMarker(_PRF_PFX + nameof(Remove));

        bool ICollection<TValue>.Remove(TValue item)
        {
            using (_PRF_Remove.Auto())
            {
                INTERNAL_INITIALIZE();

                return Remove(item);
            }
        }

        public bool IsReadOnly => false;

        private static readonly ProfilerMarker _PRF_CopyTo = new ProfilerMarker(_PRF_PFX + nameof(CopyTo));

        public void CopyTo(TValue[] array, int arrayIndex)
        {
            using (_PRF_CopyTo.Auto())
            {
                INTERNAL_INITIALIZE();

                for (var i = arrayIndex; i < array.Length; i++)
                {
                    array[i] = values[i - arrayIndex];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            INTERNAL_INITIALIZE();
            return values.GetEnumerator();
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
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

                    return values.Count;
                }
            }
        }

        private static readonly ProfilerMarker _PRF_Add = new ProfilerMarker(_PRF_PFX + nameof(Add));

        public void Add(TValue value)
        {
            using (_PRF_Add.Auto())
            {
                INTERNAL_INITIALIZE();
                INTERNAL_ADD(value);
            }
        }

        public bool Remove(TValue key)
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

        private static readonly ProfilerMarker _PRF_Indexer = new ProfilerMarker(_PRF_PFX + "Indexer");

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

        private static readonly ProfilerMarker _PRF_AddRange = new ProfilerMarker(_PRF_PFX + nameof(AddRange));

        public void AddRange(IList<TValue> vals)
        {
            using (_PRF_AddRange.Auto())
            {
                INTERNAL_INITIALIZE();

                for (var i = 0; i < vals.Count; i++)
                {
                    var value = INTERNAL_GET_VALUE_BY_INDEX(i);

                    INTERNAL_ADD(value);
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

        private static readonly ProfilerMarker _PRF_Contains = new ProfilerMarker(_PRF_PFX + nameof(Contains));

        public bool Contains(TValue item)
        {
            using (_PRF_Contains.Auto())
            {
                INTERNAL_INITIALIZE();
                return (item != null) && INTERNAL_CONTAINS(item);
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

        private static readonly ProfilerMarker _PRF_IfPresent = new ProfilerMarker(_PRF_PFX + nameof(IfPresent));

        public void IfPresent(TValue value, Action present, Action notPresent)
        {
            using (_PRF_IfPresent.Auto())
            {
                INTERNAL_INITIALIZE();

                if (INTERNAL_CONTAINS(value))
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

        public IReadOnlyList<TValue> Values => values;

        [field: NonSerialized] public int LastFrameCheck { get; private set; } = -1;

        public int InitializerCount
        {
            get => initializerCount;
            set => initializerCount = value;
        }

        public void SetDirtyAction(Action a)
        {
            _setDirtyAction = a;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            INTERNAL_INITIALIZE(false);
        }

        public void RemoveNulls()
        {
            if (values.RemoveNulls())
            {
                INTERNAL_REBUILD();
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_CLEAR = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_CLEAR));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void INTERNAL_CLEAR()
        {
            using (_PRF_INTERNAL_CLEAR.Auto())
            {
                values.Clear();
                _indices.Clear();
                _setDirtyAction?.Invoke();
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
        private bool INTERNAL_CONTAINS(TValue value)
        {
            using (_PRF_INTERNAL_CONTAINS.Auto())
            {
                return _indices.ContainsKey(value);
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_REMOVE = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_REMOVE));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TValue INTERNAL_REMOVE(int targetIndex)
        {
            using (_PRF_INTERNAL_REMOVE.Auto())
            {
                if ((targetIndex >= values.Count) || (targetIndex < 0))
                {
                    throw new IndexOutOfRangeException($"Index [{targetIndex}] is out of range for collection of length [{values.Count}].");
                }

                var deletingValue = values[targetIndex];

                _indices.Remove(deletingValue);

                var lastIndex = values.Count - 1;

                if (lastIndex != targetIndex)
                {
                    var lastValue = values[lastIndex];

                    values[targetIndex] = lastValue;

                    _indices[lastValue] = targetIndex;
                }

                values.RemoveAt(lastIndex);

                _setDirtyAction?.Invoke();
                return deletingValue;
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_ADD = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_ADD));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void INTERNAL_ADD(TValue value)
        {
            using (_PRF_INTERNAL_ADD.Auto())
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _indices.Add(value, _indices.Count);
                values.Add(value);
                _setDirtyAction?.Invoke();
            }
        }

        private static readonly ProfilerMarker _PRF_INTERNAL_INITIALIZE = new ProfilerMarker(_PRF_PFX + nameof(INTERNAL_INITIALIZE));

        private void INTERNAL_INITIALIZE(bool getFrameCount = true)
        {
            using (_PRF_INTERNAL_INITIALIZE.Auto())
            {
                if (getFrameCount)
                {
                    var frameCount = Time.frameCount;

                    if (LastFrameCheck == frameCount)
                    {
                        return;
                    }

                    LastFrameCheck = frameCount;
                }

                if (values == null)
                {
                    values = new TValueList {Capacity = initializerCount};

                    ;
                    _setDirtyAction?.Invoke();
                }

                if (_indices == null)
                {
                    _indices = new Dictionary<TValue, int>(initializerCount);
                    _setDirtyAction?.Invoke();
                }

                if (!_isValueUnityObjectChecked)
                {
                    isValueUnityObject = typeof(Object).IsAssignableFrom(typeof(TValue));
                    _isValueUnityObjectChecked = true;
                }

                //INTERNAL_CHECK_FATAL();

                if (INTERNAL_REQUIRES_REBUILD())
                {
                    INTERNAL_REBUILD();
                }

                //INTERNAL_CHECK_FATAL();

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
                    _indices.Clear();
                    _setDirtyAction?.Invoke();
                    return false;
                }

                if (isValueUnityObject)
                {
                    if (values.RemoveNulls())
                    {
                        return true;
                    }
                }

                var indexCount = _indices.Count;

                if (indexCount != valueCount)
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
                _indices.Clear();

                var newIndex = 0;

                for (var i = 0; i < values.Count; i++)
                {
                    var value = values[i];

                    _indices.Add(value, newIndex);

                    newIndex += 1;
                }

                _setDirtyAction?.Invoke();
            }
        }
    }
}