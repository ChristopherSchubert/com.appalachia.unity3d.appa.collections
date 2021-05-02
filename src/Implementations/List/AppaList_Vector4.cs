#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Vector4 : AppaList<Vector4>
    {
        public AppaList_Vector4()
        {
        }

        public AppaList_Vector4(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Vector4(AppaList<Vector4> list) : base(list)
        {
        }

        public AppaList_Vector4(Vector4[] values) : base(values)
        {
        }
    }
}
