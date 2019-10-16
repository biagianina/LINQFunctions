using System.Collections.Generic;

namespace LinqFunctions
{
    internal class Comparers<T> : IComparer<T>
    {
        readonly IComparer<T> first;
        readonly IComparer<T> second;

        public Comparers(IComparer<T> first, IComparer<T> second)
        {
            this.first = first;
            this.second = second;
        }

        public int Compare(T x, T y)
        {
            int result = first.Compare(x, y);
            return result == 0 ? second.Compare(x, y) : result;
        }
    }
}
