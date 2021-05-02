#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_ReflectionProbe : AppaList<ReflectionProbe>
    {
        public AppaList_ReflectionProbe()
        {
        }

        public AppaList_ReflectionProbe(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_ReflectionProbe(AppaList<ReflectionProbe> list) : base(list)
        {
        }

        public AppaList_ReflectionProbe(ReflectionProbe[] values) : base(values)
        {
        }
    }
}
