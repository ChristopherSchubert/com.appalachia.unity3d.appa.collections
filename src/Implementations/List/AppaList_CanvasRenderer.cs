#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_CanvasRenderer : AppaList<CanvasRenderer>
    {
        public AppaList_CanvasRenderer()
        {
        }

        public AppaList_CanvasRenderer(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_CanvasRenderer(AppaList<CanvasRenderer> list) : base(list)
        {
        }

        public AppaList_CanvasRenderer(CanvasRenderer[] values) : base(values)
        {
        }
    }
}
