#region

using System;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_ListOfGameObject : AppaList<List<GameObject>>
    {
        public AppaList_ListOfGameObject()
        {
        }

        public AppaList_ListOfGameObject(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_ListOfGameObject(AppaList<List<GameObject>> list) : base(list)
        {
        }

        public AppaList_ListOfGameObject(List<GameObject>[] values) : base(values)
        {
        }
    }
}
