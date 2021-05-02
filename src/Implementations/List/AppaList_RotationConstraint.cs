#region

using System;
using UnityEngine.Animations;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_RotationConstraint : AppaList<RotationConstraint>
    {
        public AppaList_RotationConstraint()
        {
        }

        public AppaList_RotationConstraint(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_RotationConstraint(AppaList<RotationConstraint> list) : base(list)
        {
        }

        public AppaList_RotationConstraint(RotationConstraint[] values) : base(values)
        {
        }
    }
}
