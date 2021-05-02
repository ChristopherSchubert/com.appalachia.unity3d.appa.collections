namespace collections.src.Interfaces
{
    public interface IAppaLookupSafeUpdates<TKey, TValue, TValueList> : IAppaLookupReadOnly<TKey, TValue, TValueList>, IAppaLookupAddOnly<TKey, TValue>
    where TValueList : AppaList<TValue>
    {
        new TValue this[TKey key] { get; set; }
        new TValueList at { get; set; }
    }
}
