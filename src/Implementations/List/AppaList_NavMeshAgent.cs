#region

using System;
using UnityEngine.AI;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_NavMeshAgent : AppaList<NavMeshAgent>
    {
        public AppaList_NavMeshAgent()
        {
        }

        public AppaList_NavMeshAgent(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_NavMeshAgent(AppaList<NavMeshAgent> list) : base(list)
        {
        }

        public AppaList_NavMeshAgent(NavMeshAgent[] values) : base(values)
        {
        }
    }
}
