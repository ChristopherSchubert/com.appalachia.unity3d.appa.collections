#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Color : AppaList<Color>
    {
        public AppaList_Color()
        {
        }

        public AppaList_Color(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Color(AppaList<Color> list) : base(list)
        {
        }

        public AppaList_Color(Color[] values) : base(values)
        {
        }
    }
}
