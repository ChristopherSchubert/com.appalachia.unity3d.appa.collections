using collections.src.Implementations.List;
using collections.src.Interfaces;

namespace collections.src.Extensions
{
    public static class AppaLookupExtensions
    {
        public static void AddOrIncrement<T, TKey>(this T index, TKey key, int initial)
            where T : IAppaLookup<TKey, int, AppaList_int>
        {
            if (!index.ContainsKey(key))
            {
                index.Add(key, initial);
            }
            else
            {
                index[key] += 1;
            }
        }
        
        public static void AddOrDecrement<T, TKey>(this T index, TKey key, int initial)
            where T : IAppaLookup<TKey, int, AppaList_int>
        {
            if (!index.ContainsKey(key))
            {
                index.Add(key, initial);
            }
            else
            {
                index[key] -= 1;
            }
        }
        
        
        public static int AddOrIncrementAndGet<T, TKey>(this T index, TKey key, int initial)
            where T : IAppaLookup<TKey, int, AppaList_int>
        {
            if (!index.ContainsKey(key))
            {
                index.Add(key, initial);
                return initial;
            }

            var current = index[key];
            index[key] = current + 1;
            return current + 1;
        }
        
        public static int AddOrDecrementAndGet<T, TKey>(this T index, TKey key, int initial)
            where T : IAppaLookup<TKey, int, AppaList_int>
        {
            if (!index.ContainsKey(key))
            {
                index.Add(key, initial);
                return initial;
            }

            var current = index[key];
            index[key] = current - 1;
            return current - 1;
        }
        
        public static int GetAndDecrement<T, TKey>(this T index, TKey key)
            where T : IAppaLookup<TKey, int, AppaList_int>
        {
            var current = index.Get(key);

            index[key] = current - 1;

            return current;
        }
        
        public static int GetAndIncrement<T, TKey>(this T index, TKey key)
            where T : IAppaLookup<TKey, int, AppaList_int>
        {
            var current = index.Get(key);

            index[key] = current + 1;

            return current;
        }
        
        
        public static int DecrementAndGet<T, TKey>(this T index, TKey key)
            where T : IAppaLookup<TKey, int, AppaList_int>
        {
            var current = index.Get(key);

            index[key] = current - 1;

            return current - 1;
        }
        
        public static int IncrementAndGet<T, TKey>(this T index, TKey key)
            where T : IAppaLookup<TKey, int, AppaList_int>
        {
            var current = index.Get(key);

            index[key] = current + 1;

            return current + 1;
        }
        
        public static void Toggle<T, TKey>(this T index, TKey key)
            where T : IAppaLookup<TKey, bool, AppaList_bool>
        {
            var current = index.Get(key);

            index[key] = !current;
        }
        
        public static bool GetAndToggle<T, TKey>(this T index, TKey key)
            where T : IAppaLookup<TKey, bool, AppaList_bool>
        {
            var current = index.Get(key);

            index[key] = !current;

            return current;
        }
        
        public static bool ToggleAndGet<T, TKey>(this T index, TKey key)
            where T : IAppaLookup<TKey, bool, AppaList_bool>
        {
            var current = index.Get(key);

            index[key] = !current;

            return !current;
        }
        
        
    }
}
