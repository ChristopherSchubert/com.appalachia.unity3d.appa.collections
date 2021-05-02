#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_AudioReverbFilter : AppaList<AudioReverbFilter>
    {
        public AppaList_AudioReverbFilter()
        {
        }

        public AppaList_AudioReverbFilter(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_AudioReverbFilter(AppaList<AudioReverbFilter> list) : base(list)
        {
        }

        public AppaList_AudioReverbFilter(AudioReverbFilter[] values) : base(values)
        {
        }
    }
}
