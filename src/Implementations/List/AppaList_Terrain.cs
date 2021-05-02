#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Terrain : AppaList<Terrain>
    {
        public AppaList_Terrain()
        {
        }

        public AppaList_Terrain(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Terrain(AppaList<Terrain> list) : base(list)
        {
        }

        public AppaList_Terrain(Terrain[] values) : base(values)
        {
        }
    }
}
