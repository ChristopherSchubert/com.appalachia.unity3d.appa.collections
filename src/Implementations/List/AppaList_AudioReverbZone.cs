#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_AudioReverbZone : AppaList<AudioReverbZone>
    {
        public AppaList_AudioReverbZone()
        {
        }

        public AppaList_AudioReverbZone(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_AudioReverbZone(AppaList<AudioReverbZone> list) : base(list)
        {
        }

        public AppaList_AudioReverbZone(AudioReverbZone[] values) : base(values)
        {
        }
    }
}
