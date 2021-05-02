#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Animator : AppaList<Animator>
    {
        public AppaList_Animator()
        {
        }

        public AppaList_Animator(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Animator(AppaList<Animator> list) : base(list)
        {
        }

        public AppaList_Animator(Animator[] values) : base(values)
        {
        }
    }
}
