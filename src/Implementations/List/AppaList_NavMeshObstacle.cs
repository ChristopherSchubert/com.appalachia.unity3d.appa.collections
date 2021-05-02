#region

using System;
using UnityEngine.AI;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_NavMeshObstacle : AppaList<NavMeshObstacle>
    {
        public AppaList_NavMeshObstacle()
        {
        }

        public AppaList_NavMeshObstacle(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_NavMeshObstacle(AppaList<NavMeshObstacle> list) : base(list)
        {
        }

        public AppaList_NavMeshObstacle(NavMeshObstacle[] values) : base(values)
        {
        }
    }
}
