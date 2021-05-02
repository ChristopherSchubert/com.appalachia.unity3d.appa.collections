#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_WindZone : AppaList<WindZone>
    {
        public AppaList_WindZone()
        {
        }

        public AppaList_WindZone(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_WindZone(AppaList<WindZone> list) : base(list)
        {
        }

        public AppaList_WindZone(WindZone[] values) : base(values)
        {
        }
    }
}
