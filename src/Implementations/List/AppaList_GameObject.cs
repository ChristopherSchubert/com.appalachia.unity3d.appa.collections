#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_GameObject : AppaList<GameObject>
    {
        public AppaList_GameObject()
        {
        }

        public AppaList_GameObject(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_GameObject(AppaList<GameObject> list) : base(list)
        {
        }

        public AppaList_GameObject(GameObject[] values) : base(values)
        {
        }
    }
}
