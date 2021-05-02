#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Joint : AppaList<Joint>
    {
        public AppaList_Joint()
        {
        }

        public AppaList_Joint(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Joint(AppaList<Joint> list) : base(list)
        {
        }

        public AppaList_Joint(Joint[] values) : base(values)
        {
        }
    }
}
