#region

using System;

#endregion

namespace collections.src.Exceptions
{
    public class IndexedKeyValueMismatchException : Exception
    {
        public IndexedKeyValueMismatchException(int keyCount, int valueCount) : base(
            $"Count mismatch between indexed keys [{keyCount}] and values [{valueCount}]!"
        )
        {
        }
    }
}
