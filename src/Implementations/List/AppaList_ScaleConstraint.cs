#region

using System;
using UnityEngine.Animations;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_ScaleConstraint : AppaList<ScaleConstraint>
    {
        public AppaList_ScaleConstraint()
        {
        }

        public AppaList_ScaleConstraint(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_ScaleConstraint(AppaList<ScaleConstraint> list) : base(list)
        {
        }

        public AppaList_ScaleConstraint(ScaleConstraint[] values) : base(values)
        {
        }
    }
}
