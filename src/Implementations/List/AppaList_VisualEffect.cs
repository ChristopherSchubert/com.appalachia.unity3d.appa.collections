#region

using System;
using UnityEngine.VFX;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_VisualEffect : AppaList<VisualEffect>
    {
        public AppaList_VisualEffect()
        {
        }

        public AppaList_VisualEffect(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_VisualEffect(AppaList<VisualEffect> list) : base(list)
        {
        }

        public AppaList_VisualEffect(VisualEffect[] values) : base(values)
        {
        }
    }
}
