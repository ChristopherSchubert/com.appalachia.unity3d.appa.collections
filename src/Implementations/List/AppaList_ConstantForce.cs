#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_ConstantForce : AppaList<ConstantForce>
    {
        public AppaList_ConstantForce()
        {
        }

        public AppaList_ConstantForce(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_ConstantForce(AppaList<ConstantForce> list) : base(list)
        {
        }

        public AppaList_ConstantForce(ConstantForce[] values) : base(values)
        {
        }
    }
}
