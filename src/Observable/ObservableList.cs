using System.Collections;
using System.Collections.Generic;

namespace collections.src.Observable
{
    public class ObservableList<T> : IList<T>
    {
        IList<T> m_List;

        public event ListChangedEventHandler<T> ItemAdded;
        public event ListChangedEventHandler<T> ItemRemoved;

        public T this[int index]
        {
            get { return m_List[index]; }
            set
            {
                OnEvent(ItemRemoved, index, m_List[index]);
                m_List[index] = value;
                OnEvent(ItemAdded, index, value);
            }
        }

        public int Count
        {
            get { return m_List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public ObservableList()
            : this(0) {}

        public ObservableList(int capacity)
        {
            m_List = new List<T>(capacity);
        }

        public ObservableList(IEnumerable<T> collection)
        {
            m_List = new List<T>(collection);
        }

        void OnEvent(ListChangedEventHandler<T> e, int index, T item)
        {
            if (e != null)
                e(this, new ListChangedEventArgs<T>(index, item));
        }

        public bool Contains(T item)
        {
            return m_List.Contains(item);
        }

        public int IndexOf(T item)
        {
            return m_List.IndexOf(item);
        }

        public void Add(T item)
        {
            m_List.Add(item);
            OnEvent(ItemAdded, m_List.IndexOf(item), item);
        }

        public void Add(params T[] items)
        {
            for (var index = 0; index < items.Length; index++)
            {
                var i = items[index];
                Add(i);
            }
        }

        public void Insert(int index, T item)
        {
            m_List.Insert(index, item);
            OnEvent(ItemAdded, index, item);
        }

        public bool Remove(T item)
        {
            int index = m_List.IndexOf(item);
            bool ret = m_List.Remove(item);
            if (ret)
                OnEvent(ItemRemoved, index, item);
            return ret;
        }

        public int Remove(params T[] items)
        {
            if (items == null)
                return 0;

            int count = 0;

            for (var index = 0; index < items.Length; index++)
            {
                var i = items[index];
                count += Remove(i) ? 1 : 0;
            }

            return count;
        }

        public void RemoveAt(int index)
        {
            var item = m_List[index];
            m_List.RemoveAt(index);
            OnEvent(ItemRemoved, index, item);
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
                RemoveAt(i);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_List.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
