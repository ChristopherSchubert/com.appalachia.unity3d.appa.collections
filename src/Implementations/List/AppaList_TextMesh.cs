#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_TextMesh : AppaList<TextMesh>
    {
        public AppaList_TextMesh()
        {
        }

        public AppaList_TextMesh(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_TextMesh(AppaList<TextMesh> list) : base(list)
        {
        }

        public AppaList_TextMesh(TextMesh[] values) : base(values)
        {
        }
    }
}
