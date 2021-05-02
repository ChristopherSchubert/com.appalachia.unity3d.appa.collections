#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_AudioHighPassFilter : AppaList<AudioHighPassFilter>
    {
        public AppaList_AudioHighPassFilter()
        {
        }

        public AppaList_AudioHighPassFilter(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_AudioHighPassFilter(AppaList<AudioHighPassFilter> list) : base(list)
        {
        }

        public AppaList_AudioHighPassFilter(AudioHighPassFilter[] values) : base(values)
        {
        }
    }
}
