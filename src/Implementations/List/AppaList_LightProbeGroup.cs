#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_LightProbeGroup : AppaList<LightProbeGroup>
    {
        public AppaList_LightProbeGroup()
        {
        }

        public AppaList_LightProbeGroup(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_LightProbeGroup(AppaList<LightProbeGroup> list) : base(list)
        {
        }

        public AppaList_LightProbeGroup(LightProbeGroup[] values) : base(values)
        {
        }
    }
}
