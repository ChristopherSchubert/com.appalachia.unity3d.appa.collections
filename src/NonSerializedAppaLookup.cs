#region

using System;
using collections.src.Special;
using UnityEngine;

#endregion

namespace collections.src
{
    public sealed class NonSerializedAppaLookup<TKey, TValue> : AppaLookup<TKey, TValue, NonSerializedList<TKey>, NonSerializedList<TValue>>
    {
        protected override string GetDisplayTitle(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        protected override string GetDisplaySubtitle(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        protected override Color GetDisplayColor(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }
    }
}
