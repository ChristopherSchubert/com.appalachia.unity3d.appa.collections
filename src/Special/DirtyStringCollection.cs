#region

using System;
using collections.src.Implementations.List;
using UnityEngine;

#endregion

namespace collections.src.Special
{
    [Serializable]
    public sealed class DirtyStringCollection : IsDirtyCollection<string, AppaList_string>
    {
        protected override string GetDisplayTitle(string key, bool value)
        {
            return key;
        }

        protected override string GetDisplaySubtitle(string key, bool value)
        {
            return value.ToString();
        }

        protected override Color GetDisplayColor(string key, bool value)
        {
            return Color.white;
        }
    }
}
