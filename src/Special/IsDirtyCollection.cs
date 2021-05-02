#region

using System;
using collections.src.Implementations.List;

#endregion

namespace collections.src.Special
{
    [Serializable]
    public abstract class IsDirtyCollection<T, TList> : AppaLookup<T, bool, TList, AppaList_bool>
        where T : IEquatable<T>
        where TList : AppaList<T>, new()
    {
        public bool defaultStateIsDirty = true;

        public bool IsDirty(T check)
        {
            if (!ContainsKey(check))
            {
                AddOrUpdate(check, defaultStateIsDirty);
            }

            return this[check];
        }

        public void Clean(T check)
        {
            this[check] = false;
        }

        public void CleanAll()
        {
            for (var i = 0; i < Count; i++)
            {
                at[i] = false;
            }
        }

        public void DirtyAll()
        {
            for (var i = 0; i < Count; i++)
            {
                at[i] = true;
            }
        }
    }
}
