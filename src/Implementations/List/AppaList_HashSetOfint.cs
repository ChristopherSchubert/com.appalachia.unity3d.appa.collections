#region

using System;
using System.Collections.Generic;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_HashSetOfint : AppaList<HashSet<int>>
    {
        public AppaList_HashSetOfint(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_HashSetOfint(AppaList<HashSet<int>> list) : base(list)
        {
        }

        public AppaList_HashSetOfint(HashSet<int>[] values) : base(values)
        {
        }
    }
}
