#region

using System;
using UnityEngine.AI;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_OffMeshLink : AppaList<OffMeshLink>
    {
        public AppaList_OffMeshLink()
        {
        }

        public AppaList_OffMeshLink(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_OffMeshLink(AppaList<OffMeshLink> list) : base(list)
        {
        }

        public AppaList_OffMeshLink(OffMeshLink[] values) : base(values)
        {
        }
    }
}
