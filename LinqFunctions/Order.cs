using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqFunctions
{
    class Order<T, Tkey> : IOrderedEnumerable<T>
    {
        private readonly IEnumerable<T> source;
        readonly Func<T, Tkey> keySelector;
        readonly IComparer<Tkey> comparer;

        public Order(IEnumerable<T> source, Func<T, Tkey> keySelector, IComparer<Tkey> comparer)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            List<T> elements = source.ToList();
            for (int minIndex = 0; minIndex < elements.Count - 1; minIndex++)
            {
                int minimum = GetMinimumIndex(minIndex, elements);
                InsertAtMinimumIndex(minimum, minIndex, elements);
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

        private int GetMinimumIndex(int minIndex, List<T> elements)
        {
            var minimum = minIndex;
            for (int i = minimum; i < elements.Count; i++)
            {
                minimum = GetMinimum(i, minimum, elements);
            }

            return minimum;
        }

        private int GetMinimum(int i, int minimum, List<T> elements)
        {
            return comparer.Compare(keySelector(elements[i]), keySelector(elements[minimum])) < 0 ? i : minimum;
        }

        private void InsertAtMinimumIndex(int a, int minIndex, List<T> elements)
        {
            if (minIndex == a)
            {
                return;
            }

            var minimum = elements[a];
            for (int i = a; i > minIndex; i--)
            {
                elements[i] = elements[i - 1];
            }

            elements[minIndex] = minimum;
        }
    }
}
