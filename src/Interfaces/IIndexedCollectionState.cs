#region

using System;
using System.Collections.Generic;

#endregion

namespace collections.src.Interfaces
{
    public interface IIndexedCollectionState<TValue>
    {
        IReadOnlyList<TValue> Values { get; }
        int LastFrameCheck { get; }
        int InitializerCount { get; set; }

        void SetDirtyAction(Action a);
    }
}
