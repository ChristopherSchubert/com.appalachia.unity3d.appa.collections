#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_StreamingController : AppaList<StreamingController>
    {
        public AppaList_StreamingController()
        {
        }

        public AppaList_StreamingController(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_StreamingController(AppaList<StreamingController> list) : base(list)
        {
        }

        public AppaList_StreamingController(StreamingController[] values) : base(values)
        {
        }
    }
}
