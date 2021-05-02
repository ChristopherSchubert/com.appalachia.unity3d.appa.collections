#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_CanvasGroup : AppaList<CanvasGroup>
    {
        public AppaList_CanvasGroup()
        {
        }

        public AppaList_CanvasGroup(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_CanvasGroup(AppaList<CanvasGroup> list) : base(list)
        {
        }

        public AppaList_CanvasGroup(CanvasGroup[] values) : base(values)
        {
        }
    }
}
