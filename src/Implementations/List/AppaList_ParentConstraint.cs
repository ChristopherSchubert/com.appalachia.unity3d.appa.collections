#region

using System;
using UnityEngine.Animations;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_ParentConstraint : AppaList<ParentConstraint>
    {
        public AppaList_ParentConstraint()
        {
        }

        public AppaList_ParentConstraint(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_ParentConstraint(AppaList<ParentConstraint> list) : base(list)
        {
        }

        public AppaList_ParentConstraint(ParentConstraint[] values) : base(values)
        {
        }
    }
}
