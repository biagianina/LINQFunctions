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
                Swap(minIndex, elements);
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

        private void Swap(int minIndex, List<T> elements)
        {
            for (int i = minIndex + 1; i < elements.Count; i++)
            {
                if (comparer.Compare(keySelector(elements[i]), keySelector(elements[minIndex])) < 0)
                {
                    elements.Insert(minIndex, elements[i]);
                    elements.RemoveAt(i + 1);
                }
            }
        }
    }
}
