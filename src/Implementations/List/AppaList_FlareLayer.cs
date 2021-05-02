#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_FlareLayer : AppaList<FlareLayer>
    {
        public AppaList_FlareLayer()
        {
        }

        public AppaList_FlareLayer(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_FlareLayer(AppaList<FlareLayer> list) : base(list)
        {
        }

        public AppaList_FlareLayer(FlareLayer[] values) : base(values)
        {
        }
    }
}
