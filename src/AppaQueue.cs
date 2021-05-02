#region

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

#endregion

namespace collections.src
{
    [Serializable]
    public abstract class AppaQueue<T> : 
                                        ISerializationCallbackReceiver,
                                        IEnumerable<T>,
                                        IEnumerable,
                                        ICollection,
                                        IReadOnlyCollection<T>
    {
        private const string _PRF_PFX = nameof(AppaQueue<T>) + ".";
        
        [SerializeField, HideInInspector]
        private T[] _array;

        private Queue<T> _queue;

        public AppaQueue()
        {
            _queue = new Queue<T>();
        }

        public int Count => _queue.Count;

        public void CopyTo(Array array, int index)
        {
            using (_PRF_CopyTo.Auto())
            {
                ((ICollection) _queue).CopyTo(array, index);
            }
        }

        int ICollection.Count => _queue.Count;

        public bool IsSynchronized => ((ICollection) _queue).IsSynchronized;

        public object SyncRoot => ((ICollection) _queue).SyncRoot;

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _queue).GetEnumerator();
        }

        int IReadOnlyCollection<T>.Count => _queue.Count;

        private static readonly ProfilerMarker _PRF_OnBeforeSerialize = new ProfilerMarker(_PRF_PFX + nameof(OnBeforeSerialize));
        public void OnBeforeSerialize()
        {
            using (_PRF_OnBeforeSerialize.Auto())
            {
                var length = _queue.Count;

                _array = new T[length];

                for (var i = 0; i < length; i++)
                {
                    _array[i] = _queue.Dequeue();
                }
            }
        }

        private static readonly ProfilerMarker _PRF_OnAfterDeserialize = new ProfilerMarker(_PRF_PFX + nameof(OnAfterDeserialize));
        public void OnAfterDeserialize()
        {
            using (_PRF_OnAfterDeserialize.Auto())
            {
                _queue = new Queue<T>();

                for (var i = 0; i < _array.Length; i++)
                {
                    _queue.Enqueue(_array[i]);
                }

                _array = null;
            }
        }

        private static readonly ProfilerMarker _PRF_Initialize = new ProfilerMarker(_PRF_PFX + nameof(Initialize));
        private void Initialize()
        {
            using (_PRF_Initialize.Auto())
            {
                if (_queue == null)
                {
                    _queue = new Queue<T>();
                }
            }
        }

        private static readonly ProfilerMarker _PRF_Clear = new ProfilerMarker(_PRF_PFX + nameof(Clear));
        public virtual void Clear()
        {
            using (_PRF_Clear.Auto())
            {
                Initialize();
                _queue.Clear();
            }
        }

        private static readonly ProfilerMarker _PRF_CopyTo = new ProfilerMarker(_PRF_PFX + nameof(CopyTo));
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            using (_PRF_CopyTo.Auto())
            {
                _queue.CopyTo(array, arrayIndex);
            }
        }

        private static readonly ProfilerMarker _PRF_Enqueue = new ProfilerMarker(_PRF_PFX + nameof(Enqueue));
        public virtual void Enqueue(T item)
        {
            using (_PRF_Enqueue.Auto())
            {
                Initialize();
                _queue.Enqueue(item);
            }
        }

        private static readonly ProfilerMarker _PRF_Dequeue = new ProfilerMarker(_PRF_PFX + nameof(Dequeue));
        public virtual T Dequeue()
        {
            using (_PRF_Dequeue.Auto())
            {
                Initialize();
                return _queue.Dequeue();
            }
        }

        private static readonly ProfilerMarker _PRF_Peek = new ProfilerMarker(_PRF_PFX + nameof(Peek));
        public virtual T Peek()
        {
            using (_PRF_Peek.Auto())
            {
                Initialize();
                return _queue.Peek();
            }
        }

        private static readonly ProfilerMarker _PRF_Contains = new ProfilerMarker(_PRF_PFX + nameof(Contains));
        public virtual bool Contains(T item)
        {
            using (_PRF_Contains.Auto())
            {
                Initialize();
                return _queue.Contains(item);
            }
        }

        private static readonly ProfilerMarker _PRF_ToArray = new ProfilerMarker(_PRF_PFX + nameof(ToArray));
        public virtual T[] ToArray()
        {
            using (_PRF_ToArray.Auto())
            {
                return _queue.ToArray();
            }
        }

        private static readonly ProfilerMarker _PRF_TrimExcess = new ProfilerMarker(_PRF_PFX + nameof(TrimExcess));
        public virtual void TrimExcess()
        {
            using (_PRF_TrimExcess.Auto())
            {
                _queue.TrimExcess();
            }
        }
    }
}
