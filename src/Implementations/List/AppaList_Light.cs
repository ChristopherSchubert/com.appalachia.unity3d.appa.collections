#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Light : AppaList<Light>
    {
        public AppaList_Light()
        {
        }

        public AppaList_Light(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Light(AppaList<Light> list) : base(list)
        {
        }

        public AppaList_Light(Light[] values) : base(values)
        {
        }
    }
}
