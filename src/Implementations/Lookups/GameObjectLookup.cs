#region

using System;
using collections.src.Implementations.List;
using UnityEngine;

#endregion

namespace collections.src.Implementations.Lookups
{
    [Serializable]
    public class GameObjectLookup : AppaLookup<int, GameObject, AppaList_int, AppaList_GameObject>
    {
        protected override string GetDisplayTitle(int key, GameObject value)
        {
            return value == null ? "MISSING" : value.name;
        }

        protected override string GetDisplaySubtitle(int key, GameObject value)
        {
            return $"InstancedID {key})";
        }

        protected override Color GetDisplayColor(int key, GameObject value)
        {
            return Color.white;
        }
    }
}
