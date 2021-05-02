#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_AudioEchoFilter : AppaList<AudioEchoFilter>
    {
        public AppaList_AudioEchoFilter()
        {
        }

        public AppaList_AudioEchoFilter(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_AudioEchoFilter(AppaList<AudioEchoFilter> list) : base(list)
        {
        }

        public AppaList_AudioEchoFilter(AudioEchoFilter[] values) : base(values)
        {
        }
    }
}
