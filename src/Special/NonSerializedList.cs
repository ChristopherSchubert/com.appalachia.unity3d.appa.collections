namespace collections.src.Special
{
    public sealed class NonSerializedList<T> : AppaList<T>
    {
        public NonSerializedList()
        {
        }

        public NonSerializedList(int capacity, float capacityIncreaseMultiplier = 2, bool noTracking = false) : base(
            capacity,
            capacityIncreaseMultiplier,
            noTracking
        )
        {
        }

        public NonSerializedList(AppaList<T> list) : base(list)
        {
        }

        public NonSerializedList(T[] values) : base(values)
        {
        }
    }
}
