#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Rigidbody2D : AppaList<Rigidbody2D>
    {
        public AppaList_Rigidbody2D()
        {
        }

        public AppaList_Rigidbody2D(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Rigidbody2D(AppaList<Rigidbody2D> list) : base(list)
        {
        }

        public AppaList_Rigidbody2D(Rigidbody2D[] values) : base(values)
        {
        }
    }
}
