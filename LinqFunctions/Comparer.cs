using System;
using System.Collections.Generic;

namespace LinqFunctions
{
    public class MyComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            return x != null && y != null && x.Equals(y);
        }

        public int GetHashCode(T obj)
        {
            return obj != null ? obj.GetHashCode() : 0;
        }
    }
}
