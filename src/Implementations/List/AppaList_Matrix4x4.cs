#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Matrix4x4 : AppaList<Matrix4x4>
    {
        public AppaList_Matrix4x4()
        {
        }

        public AppaList_Matrix4x4(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Matrix4x4(AppaList<Matrix4x4> list) : base(list)
        {
        }

        public AppaList_Matrix4x4(Matrix4x4[] values) : base(values)
        {
        }
    }
}
