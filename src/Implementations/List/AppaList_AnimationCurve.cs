#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_AnimationCurve : AppaList<AnimationCurve>
    {
        public AppaList_AnimationCurve()
        {
        }

        public AppaList_AnimationCurve(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_AnimationCurve(AppaList<AnimationCurve> list) : base(list)
        {
        }

        public AppaList_AnimationCurve(AnimationCurve[] values) : base(values)
        {
        }
    }
}
