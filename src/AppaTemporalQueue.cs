#region

using System;
using Unity.Profiling;
using UnityEngine;

#endregion

namespace collections.src
{
    [Serializable]
    public class AppaTemporalQueue<T> : AppaQueue<T>
    {
        private const string _PRF_PFX = nameof(AppaTemporalQueue<T>) + ".";
        
        [SerializeField] private T _current;

        public T Current => _current;

        public bool HasCurrent => _current?.Equals(default) ?? false;

        private static readonly ProfilerMarker _PRF_Dequeue = new ProfilerMarker(_PRF_PFX + nameof(Dequeue));
        public override T Dequeue()
        {
            using (_PRF_Dequeue.Auto())
            {
                _current = base.Dequeue();

                return _current;
            }
        }

        private static readonly ProfilerMarker _PRF_ResetCurrent = new ProfilerMarker(_PRF_PFX + nameof(ResetCurrent));
        public void ResetCurrent()
        {
            using (_PRF_ResetCurrent.Auto())
            {
                _current = default;
            }
        }

        private static readonly ProfilerMarker _PRF_CurrentOrNext = new ProfilerMarker(_PRF_PFX + nameof(CurrentOrNext));
        public T CurrentOrNext()
        {
            using (_PRF_CurrentOrNext.Auto())
            {
                if (_current != null)
                {
                    return _current;
                }

                if (Count == 0)
                {
                    return default;
                }

                return Dequeue();
            }
        }
    }
}
