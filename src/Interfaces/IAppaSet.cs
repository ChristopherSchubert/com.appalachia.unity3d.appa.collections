#region

using System.Collections.Generic;

#endregion

namespace collections.src.Interfaces
{
    public interface IAppaSet<TValue> : IAppaSetReadOnly<TValue>
    {
        bool Remove(TValue value);
        TValue RemoveAt(int targetIndex);
        void Add(TValue value);
        void AddRange(IList<TValue> values);
        void Clear();
    }
}
