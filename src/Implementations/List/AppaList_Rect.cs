#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Rect : AppaList<Rect>
    {
        public AppaList_Rect()
        {
        }

        public AppaList_Rect(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Rect(AppaList<Rect> list) : base(list)
        {
        }

        public AppaList_Rect(Rect[] values) : base(values)
        {
        }
    }
}
