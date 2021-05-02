#region

using System;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_bool : AppaList<bool>
    {
        public AppaList_bool()
        {
        }

        public AppaList_bool(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_bool(AppaList<bool> list) : base(list)
        {
        }

        public AppaList_bool(bool[] values) : base(values)
        {
        }
    }
}
