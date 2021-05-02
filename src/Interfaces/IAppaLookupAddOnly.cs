#region

using System;
using System.Collections.Generic;

#endregion

namespace collections.src.Interfaces
{
    public interface IAppaLookupAddOnly<TKey, TValue>
    {
        void AddIfKeyNotPresent(TKey key, Func<TValue> value);
        void AddIfKeyNotPresent(TKey key, TValue value);
        void AddOrUpdate(KeyValuePair<TKey, TValue> pair);
        void AddOrUpdate(TKey key, Func<TValue> add, Func<TValue> update);
        void Add(TKey key, TValue value);
        void AddOrUpdate(TKey key, TValue value);
        void AddOrUpdateIf(TKey key, Func<TValue> valueRetriever, Predicate<TValue> updateIf);
        void AddOrUpdateIf(TKey key, TValue value, Predicate<TValue> updateIf);
        void AddOrUpdateRange(IList<TValue> values, Func<TValue, TKey> selector);
    }
}
