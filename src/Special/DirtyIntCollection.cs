#region

using System;
using collections.src.Implementations.List;
using UnityEngine;

#endregion

namespace collections.src.Special
{
    [Serializable]
    public sealed class DirtyIntCollection : IsDirtyCollection<int, AppaList_int>
    {
        protected override string GetDisplayTitle(int key, bool value)
        {
            throw new NotImplementedException();
        }

        protected override string GetDisplaySubtitle(int key, bool value)
        {
            throw new NotImplementedException();
        }

        protected override Color GetDisplayColor(int key, bool value)
        {
            return Color.white;
        }
    }
}
