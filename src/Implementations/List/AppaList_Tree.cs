#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Tree : AppaList<Tree>
    {
        public AppaList_Tree()
        {
        }

        public AppaList_Tree(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Tree(AppaList<Tree> list) : base(list)
        {
        }

        public AppaList_Tree(Tree[] values) : base(values)
        {
        }
    }
}
