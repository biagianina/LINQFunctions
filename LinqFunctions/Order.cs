using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqFunctions
{
    class Order<TSource, TKey> : IOrderedEnumerable<TSource>
    {
        private readonly IEnumerable<TSource> source;
        private readonly IComparer<TSource> comparer;

        public Order(IEnumerable<TSource> source, IComparer<TSource> initialComparer)
        {
            this.source = source;
            comparer = initialComparer;
        }

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
        {
            var comparers = new Comparers<TSource>(comparer, new KeyComparer<TSource, TKey>(keyComparer, keySelector));
            return new Order<TSource, TKey>(this, comparers);
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            List<TSource> elements = source.ToList();
            for (int minIndex = 0; minIndex < elements.Count - 1; minIndex++)
            {
                int minimum = GetMinimumIndex(minIndex, elements);
                Swap(minimum, minIndex, elements);
            }

            foreach (var item in elements)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int GetMinimumIndex(int minIndex, List<TSource> elements)
        {
            var minimum = minIndex;
            for (int i = minimum; i < elements.Count; i++)
            {
                minimum = GetMinimum(i, minimum, elements);
            }

            return minimum;
        }

        private int GetMinimum(int i, int minimum, List<TSource> elements)
        {
            return comparer.Compare(elements[i], elements[minimum]) < 0 ? i : minimum;
        }

        private void Swap(int minimum, int minIndex, List<TSource> elements)
        {
            if (minIndex == minimum)
            {
                return;
            }

            var temp = elements[minimum];
            elements[minimum] = elements[minIndex];
            elements[minIndex] = temp;
        }
    }
}
