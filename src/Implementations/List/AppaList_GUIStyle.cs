#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_GUIStyle : AppaList<GUIStyle>
    {
        public AppaList_GUIStyle()
        {
        }

        public AppaList_GUIStyle(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_GUIStyle(AppaList<GUIStyle> list) : base(list)
        {
        }

        public AppaList_GUIStyle(GUIStyle[] values) : base(values)
        {
        }
    }
}
