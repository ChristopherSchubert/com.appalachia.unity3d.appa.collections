#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_AudioBehaviour : AppaList<AudioBehaviour>
    {
        public AppaList_AudioBehaviour()
        {
        }

        public AppaList_AudioBehaviour(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_AudioBehaviour(AppaList<AudioBehaviour> list) : base(list)
        {
        }

        public AppaList_AudioBehaviour(AudioBehaviour[] values) : base(values)
        {
        }
    }
}
