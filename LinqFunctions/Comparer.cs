using System;
using System.Collections.Generic;

namespace LinqFunctions
{
    public class MyComparer<T> : IComparer<T>
    {
        readonly Func<T, T, int> comp;

        public MyComparer(Func<T, T, int> comp)
        {
            this.comp = comp;
        }

        public int Compare(T x, T y)
        {
            return comp(x, y);
        }
    }
}
