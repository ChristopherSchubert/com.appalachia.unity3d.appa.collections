#region

using System;
using collections.src.Implementations.List;
using UnityEngine;

#endregion

namespace collections.src.Implementations.Lookups
{
    [Serializable]
    public class GameObjectReplacementLookup : AppaLookup<GameObject, GameObject, AppaList_GameObject, AppaList_GameObject>
    {
        protected override string GetDisplayTitle(GameObject key, GameObject value)
        {
            return $"Replacing: {key.name}";
        }

        protected override string GetDisplaySubtitle(GameObject key, GameObject value)
        {
            return $"Replacement: {value.name}";
        }

        protected override Color GetDisplayColor(GameObject key, GameObject value)
        {
            return Color.white;
        }
    }
}
