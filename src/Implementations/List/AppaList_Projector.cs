#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Projector : AppaList<Projector>
    {
        public AppaList_Projector()
        {
        }

        public AppaList_Projector(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Projector(AppaList<Projector> list) : base(list)
        {
        }

        public AppaList_Projector(Projector[] values) : base(values)
        {
        }
    }
}
