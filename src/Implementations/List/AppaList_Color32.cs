#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Color32 : AppaList<Color32>
    {
        public AppaList_Color32()
        {
        }

        public AppaList_Color32(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Color32(AppaList<Color32> list) : base(list)
        {
        }

        public AppaList_Color32(Color32[] values) : base(values)
        {
        }
    }
}
