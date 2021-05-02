#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Effector2D : AppaList<Effector2D>
    {
        public AppaList_Effector2D()
        {
        }

        public AppaList_Effector2D(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Effector2D(AppaList<Effector2D> list) : base(list)
        {
        }

        public AppaList_Effector2D(Effector2D[] values) : base(values)
        {
        }
    }
}
