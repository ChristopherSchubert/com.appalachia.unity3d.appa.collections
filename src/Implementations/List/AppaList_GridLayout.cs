#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_GridLayout : AppaList<GridLayout>
    {
        public AppaList_GridLayout()
        {
        }

        public AppaList_GridLayout(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_GridLayout(AppaList<GridLayout> list) : base(list)
        {
        }

        public AppaList_GridLayout(GridLayout[] values) : base(values)
        {
        }
    }
}
