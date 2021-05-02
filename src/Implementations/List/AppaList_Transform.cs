#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Transform : AppaList<Transform>
    {
        public AppaList_Transform()
        {
        }

        public AppaList_Transform(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Transform(AppaList<Transform> list) : base(list)
        {
        }

        public AppaList_Transform(Transform[] values) : base(values)
        {
        }
    }
}
