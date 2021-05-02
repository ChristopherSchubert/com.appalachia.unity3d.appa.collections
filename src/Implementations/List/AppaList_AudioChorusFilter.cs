#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_AudioChorusFilter : AppaList<AudioChorusFilter>
    {
        public AppaList_AudioChorusFilter()
        {
        }

        public AppaList_AudioChorusFilter(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_AudioChorusFilter(AppaList<AudioChorusFilter> list) : base(list)
        {
        }

        public AppaList_AudioChorusFilter(AudioChorusFilter[] values) : base(values)
        {
        }
    }
}
