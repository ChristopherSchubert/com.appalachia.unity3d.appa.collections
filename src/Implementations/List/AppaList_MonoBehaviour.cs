#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_MonoBehaviour : AppaList<MonoBehaviour>
    {
        public AppaList_MonoBehaviour()
        {
        }

        public AppaList_MonoBehaviour(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_MonoBehaviour(AppaList<MonoBehaviour> list) : base(list)
        {
        }

        public AppaList_MonoBehaviour(MonoBehaviour[] values) : base(values)
        {
        }
    }
}
