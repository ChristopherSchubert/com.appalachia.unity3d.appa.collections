#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Collider : AppaList<Collider>
    {
        public AppaList_Collider()
        {
        }

        public AppaList_Collider(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Collider(AppaList<Collider> list) : base(list)
        {
        }

        public AppaList_Collider(Collider[] values) : base(values)
        {
        }
    }
}
