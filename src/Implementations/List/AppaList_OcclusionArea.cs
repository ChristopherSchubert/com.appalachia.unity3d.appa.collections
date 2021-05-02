#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_OcclusionArea : AppaList<OcclusionArea>
    {
        public AppaList_OcclusionArea()
        {
        }

        public AppaList_OcclusionArea(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_OcclusionArea(AppaList<OcclusionArea> list) : base(list)
        {
        }

        public AppaList_OcclusionArea(OcclusionArea[] values) : base(values)
        {
        }
    }
}
