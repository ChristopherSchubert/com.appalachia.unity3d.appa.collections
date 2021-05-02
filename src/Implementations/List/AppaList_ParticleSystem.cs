#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_ParticleSystem : AppaList<ParticleSystem>
    {
        public AppaList_ParticleSystem()
        {
        }

        public AppaList_ParticleSystem(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_ParticleSystem(AppaList<ParticleSystem> list) : base(list)
        {
        }

        public AppaList_ParticleSystem(ParticleSystem[] values) : base(values)
        {
        }
    }
}
