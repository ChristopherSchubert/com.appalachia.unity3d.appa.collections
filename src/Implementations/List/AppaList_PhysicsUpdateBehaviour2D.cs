#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_PhysicsUpdateBehaviour2D : AppaList<PhysicsUpdateBehaviour2D>
    {
        public AppaList_PhysicsUpdateBehaviour2D()
        {
        }

        public AppaList_PhysicsUpdateBehaviour2D(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_PhysicsUpdateBehaviour2D(AppaList<PhysicsUpdateBehaviour2D> list) : base(list)
        {
        }

        public AppaList_PhysicsUpdateBehaviour2D(PhysicsUpdateBehaviour2D[] values) : base(values)
        {
        }
    }
}
