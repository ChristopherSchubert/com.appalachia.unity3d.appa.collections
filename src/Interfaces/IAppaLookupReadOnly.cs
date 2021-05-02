#region

using System;
using System.Collections.Generic;

#endregion

namespace collections.src.Interfaces
{
    public interface IAppaLookupReadOnly<TKey, TValue, TValueList>
    where TValueList : AppaList<TValue>
    {
        IReadOnlyDictionary<TKey, TValue> Lookup { get; }
        TValue this[TKey key] { get; }
        TValueList at { get; set; }
        int Count { get; }
        TKey GetKeyByIndex(int i);
        TValue Get(TKey key);
        TValue GetByIndex(int i);
        bool ContainsKey(TKey key);
        bool TryGet(TKey key, out TValue value);
        int SumCounts(Func<TValue, int> counter);
        void IfPresent(TKey key, Action present, Action notPresent);
    }
}
