#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Rigidbody : AppaList<Rigidbody>
    {
        public AppaList_Rigidbody()
        {
        }

        public AppaList_Rigidbody(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Rigidbody(AppaList<Rigidbody> list) : base(list)
        {
        }

        public AppaList_Rigidbody(Rigidbody[] values) : base(values)
        {
        }
    }
}
