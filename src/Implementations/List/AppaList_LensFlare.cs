#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_LensFlare : AppaList<LensFlare>
    {
        public AppaList_LensFlare()
        {
        }

        public AppaList_LensFlare(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_LensFlare(AppaList<LensFlare> list) : base(list)
        {
        }

        public AppaList_LensFlare(LensFlare[] values) : base(values)
        {
        }
    }
}
