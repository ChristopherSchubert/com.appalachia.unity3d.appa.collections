#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Behaviour : AppaList<Behaviour>
    {
        public AppaList_Behaviour()
        {
        }

        public AppaList_Behaviour(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Behaviour(AppaList<Behaviour> list) : base(list)
        {
        }

        public AppaList_Behaviour(Behaviour[] values) : base(values)
        {
        }
    }
}
