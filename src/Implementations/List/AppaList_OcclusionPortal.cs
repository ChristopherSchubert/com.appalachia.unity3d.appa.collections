#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_OcclusionPortal : AppaList<OcclusionPortal>
    {
        public AppaList_OcclusionPortal()
        {
        }

        public AppaList_OcclusionPortal(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_OcclusionPortal(AppaList<OcclusionPortal> list) : base(list)
        {
        }

        public AppaList_OcclusionPortal(OcclusionPortal[] values) : base(values)
        {
        }
    }
}
