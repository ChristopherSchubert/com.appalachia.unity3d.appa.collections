#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Joint2D : AppaList<Joint2D>
    {
        public AppaList_Joint2D()
        {
        }

        public AppaList_Joint2D(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Joint2D(AppaList<Joint2D> list) : base(list)
        {
        }

        public AppaList_Joint2D(Joint2D[] values) : base(values)
        {
        }
    }
}
