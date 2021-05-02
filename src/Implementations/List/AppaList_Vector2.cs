#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Vector2 : AppaList<Vector2>
    {
        public AppaList_Vector2()
        {
        }

        public AppaList_Vector2(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Vector2(AppaList<Vector2> list) : base(list)
        {
        }

        public AppaList_Vector2(Vector2[] values) : base(values)
        {
        }
    }
}
