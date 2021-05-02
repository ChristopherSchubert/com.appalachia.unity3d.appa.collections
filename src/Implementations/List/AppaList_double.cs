#region

using System;

#endregion

namespace collections.src.Implementations.List
{
    [Serializable]
    public sealed class AppaList_double : AppaList<double>
    {
        public AppaList_double()
        {
        }

        public AppaList_double(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public AppaList_double(AppaList<double> list) : base(list)
        {
        }

        public AppaList_double(double[] values) : base(values)
        {
        }
    }
}
