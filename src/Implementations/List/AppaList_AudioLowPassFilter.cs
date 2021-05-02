#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_AudioLowPassFilter : AppaList<AudioLowPassFilter>
    {
        public AppaList_AudioLowPassFilter()
        {
        }

        public AppaList_AudioLowPassFilter(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_AudioLowPassFilter(AppaList<AudioLowPassFilter> list) : base(list)
        {
        }

        public AppaList_AudioLowPassFilter(AudioLowPassFilter[] values) : base(values)
        {
        }
    }
}
