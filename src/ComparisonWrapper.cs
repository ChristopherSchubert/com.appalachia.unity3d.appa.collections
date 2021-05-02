using System;
using System.Collections.Generic;

namespace collections.src
{
    public class ComparisonWrapper<T> : IComparer<T>
    {
        public ComparisonWrapper(Comparison<T> comparison)
        {
            _comparison = comparison;
        }

        private readonly Comparison<T> _comparison;
        
        public int Compare(T x, T y)
        {
            return _comparison?.Invoke(x, y) ?? 0;
        }
    }
}
