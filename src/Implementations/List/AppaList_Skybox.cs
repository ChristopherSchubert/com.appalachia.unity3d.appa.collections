#region

using System;
using UnityEngine;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_Skybox : AppaList<Skybox>
    {
        public AppaList_Skybox()
        {
        }

        public AppaList_Skybox(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_Skybox(AppaList<Skybox> list) : base(list)
        {
        }

        public AppaList_Skybox(Skybox[] values) : base(values)
        {
        }
    }
}
