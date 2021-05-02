#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Renderer : AppaList<Renderer>
    {
        public AppaList_Renderer()
        {
        }

        public AppaList_Renderer(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Renderer(AppaList<Renderer> list) : base(list)
        {
        }

        public AppaList_Renderer(Renderer[] values) : base(values)
        {
        }
    }
}
