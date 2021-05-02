#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Quaternion : AppaList<Quaternion>
    {
        public AppaList_Quaternion()
        {
        }

        public AppaList_Quaternion(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Quaternion(AppaList<Quaternion> list) : base(list)
        {
        }

        public AppaList_Quaternion(Quaternion[] values) : base(values)
        {
        }
    }
}
