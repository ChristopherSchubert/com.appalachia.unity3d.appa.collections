#region

using System;
using UnityEngine.Video;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_VideoPlayer : AppaList<VideoPlayer>
    {
        public AppaList_VideoPlayer()
        {
        }

        public AppaList_VideoPlayer(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_VideoPlayer(AppaList<VideoPlayer> list) : base(list)
        {
        }

        public AppaList_VideoPlayer(VideoPlayer[] values) : base(values)
        {
        }
    }
}
