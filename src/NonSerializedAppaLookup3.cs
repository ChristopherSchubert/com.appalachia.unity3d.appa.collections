using collections.src.Special;
using UnityEngine;

namespace collections.src
{
    public sealed class NonSerializedAppaLookup3<TKey1, TKey2, TKey3, TValue> : AppaLookup3<TKey1, TKey2, TKey3, TValue,
        NonSerializedList<TKey1>, 
        NonSerializedList<TKey2>, 
        NonSerializedList<TKey3>,
         NonSerializedList<TValue>, 
        NonSerializedAppaLookup<TKey3, TValue>,
        NonSerializedAppaLookup2<TKey2, TKey3, TValue>,
        NonSerializedList<NonSerializedAppaLookup<TKey3, TValue>>, 
        NonSerializedList<NonSerializedAppaLookup2<TKey2, TKey3, TValue>>>
    {
        protected override string GetDisplayTitle(TKey1 key, NonSerializedAppaLookup2<TKey2, TKey3, TValue> value)
        {
            return key.ToString();
        }

        protected override string GetDisplaySubtitle(TKey1 key, NonSerializedAppaLookup2<TKey2, TKey3, TValue> value)
        {
            return $"{value.Count} sub-values";
        }

        protected override Color GetDisplayColor(TKey1 key, NonSerializedAppaLookup2<TKey2, TKey3, TValue> value)
        {
            return Color.white;
        }
    }
}