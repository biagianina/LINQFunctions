using System;
using System.Collections.Generic;

namespace LinqFunctions
{
    public class KeyComparer<T, TKey> : IComparer<T>
    {
        readonly IComparer<TKey> comparer;
        readonly Func<T, TKey> keySelector;

        public KeyComparer(IComparer<TKey> comparer, Func<T, TKey> keySelector)
        {
            this.comparer = comparer;
            this.keySelector = keySelector;
        }

        public int Compare(T x, T y)
        {
            return comparer.Compare(keySelector(x), keySelector(y));
        }
    }
}
