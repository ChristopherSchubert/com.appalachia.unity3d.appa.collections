#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_LODGroup : AppaList<LODGroup>
    {
        public AppaList_LODGroup()
        {
        }

        public AppaList_LODGroup(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_LODGroup(AppaList<LODGroup> list) : base(list)
        {
        }

        public AppaList_LODGroup(LODGroup[] values) : base(values)
        {
        }
    }
}
