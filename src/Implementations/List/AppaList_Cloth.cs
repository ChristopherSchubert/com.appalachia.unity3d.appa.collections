#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Cloth : AppaList<Cloth>
    {
        public AppaList_Cloth()
        {
        }

        public AppaList_Cloth(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Cloth(AppaList<Cloth> list) : base(list)
        {
        }

        public AppaList_Cloth(Cloth[] values) : base(values)
        {
        }
    }
}
