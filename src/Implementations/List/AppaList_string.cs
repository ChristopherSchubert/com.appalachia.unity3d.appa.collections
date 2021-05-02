#region

using System;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_string : AppaList<string>
    {
        public AppaList_string()
        {
        }

        public AppaList_string(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_string(AppaList<string> list) : base(list)
        {
        }

        public AppaList_string(string[] values) : base(values)
        {
        }
    }
}
