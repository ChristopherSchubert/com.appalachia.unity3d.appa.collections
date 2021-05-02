#region

using System;
using UnityEngine.Animations;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_PositionConstraint : AppaList<PositionConstraint>
    {
        public AppaList_PositionConstraint()
        {
        }

        public AppaList_PositionConstraint(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_PositionConstraint(AppaList<PositionConstraint> list) : base(list)
        {
        }

        public AppaList_PositionConstraint(PositionConstraint[] values) : base(values)
        {
        }
    }
}
