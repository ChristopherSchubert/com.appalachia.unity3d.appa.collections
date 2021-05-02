#region

using System;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_int : AppaList<int>
    {
        public AppaList_int()
        {
        }

        public AppaList_int(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_int(AppaList<int> list) : base(list)
        {
        }

        public AppaList_int(int[] values) : base(values)
        {
        }
    }
}
