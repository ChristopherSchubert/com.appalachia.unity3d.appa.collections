#region

using System.Collections.Generic;

#endregion

namespace collections.src.Interfaces
{
    public interface IAppaLookupState<TKey, TValue> : IIndexedCollectionState<TValue>
    {
        IReadOnlyList<TKey> Keys { get; }
        IReadOnlyDictionary<TKey, TValue> Lookup { get; }
        IReadOnlyDictionary<TKey, int> Indices { get; }
    }
}
