#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_RectOffset : AppaList<RectOffset>
    {
        public AppaList_RectOffset()
        {
        }

        public AppaList_RectOffset(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_RectOffset(AppaList<RectOffset> list) : base(list)
        {
        }

        public AppaList_RectOffset(RectOffset[] values) : base(values)
        {
        }
    }
}
