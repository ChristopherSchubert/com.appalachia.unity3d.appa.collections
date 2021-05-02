using collections.src.Special;
using UnityEngine;

namespace collections.src
{
    public sealed class NonSerializedAppaLookup2<TKey1, TKey2, TValue> : 
        AppaLookup2<TKey1, TKey2, TValue, 
            NonSerializedList<TKey1>,
        NonSerializedList<TKey2>, 
        NonSerializedList<TValue>, 
            NonSerializedAppaLookup<TKey2, TValue>, 
            NonSerializedList<NonSerializedAppaLookup<TKey2, TValue>>
        >
    {
        protected override string GetDisplayTitle(TKey1 key, NonSerializedAppaLookup<TKey2, TValue> value)
        {
            return key.ToString();
        }

        protected override string GetDisplaySubtitle(TKey1 key, NonSerializedAppaLookup<TKey2, TValue> value)
        {
            return $"{value.Count} sub-values";
        }

        protected override Color GetDisplayColor(TKey1 key, NonSerializedAppaLookup<TKey2, TValue> value)
        {
            return Color.white;
        }
    }
}
