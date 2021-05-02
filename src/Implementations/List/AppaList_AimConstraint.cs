#region

using System;
using UnityEngine.Animations;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_AimConstraint : AppaList<AimConstraint>
    {
        public AppaList_AimConstraint()
        {
        }

        public AppaList_AimConstraint(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_AimConstraint(AppaList<AimConstraint> list) : base(list)
        {
        }

        public AppaList_AimConstraint(AimConstraint[] values) : base(values)
        {
        }
    }
}
