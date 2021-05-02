#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Canvas : AppaList<Canvas>
    {
        public AppaList_Canvas()
        {
        }

        public AppaList_Canvas(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Canvas(AppaList<Canvas> list) : base(list)
        {
        }

        public AppaList_Canvas(Canvas[] values) : base(values)
        {
        }
    }
}
