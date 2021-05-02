#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Gradient : AppaList<Gradient>
    {
        public AppaList_Gradient()
        {
        }

        public AppaList_Gradient(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Gradient(AppaList<Gradient> list) : base(list)
        {
        }

        public AppaList_Gradient(Gradient[] values) : base(values)
        {
        }
    }
}
