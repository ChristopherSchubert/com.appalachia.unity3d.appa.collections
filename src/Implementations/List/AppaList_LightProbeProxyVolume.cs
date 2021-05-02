#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_LightProbeProxyVolume : AppaList<LightProbeProxyVolume>
    {
        public AppaList_LightProbeProxyVolume()
        {
        }

        public AppaList_LightProbeProxyVolume(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_LightProbeProxyVolume(AppaList<LightProbeProxyVolume> list) : base(list)
        {
        }

        public AppaList_LightProbeProxyVolume(LightProbeProxyVolume[] values) : base(values)
        {
        }
    }
}
