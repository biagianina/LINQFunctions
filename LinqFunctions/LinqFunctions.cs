using System;
using System.Collections.Generic;

namespace LinqFunctions
{
    public static class Functions
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckNull(source);
            CheckNull(predicate);

            foreach (var s in source)
            {
                if (!predicate(s))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckNull(source);
            CheckNull(predicate);
            foreach (var s in source)
            {
                if (predicate(s))
                {
                    return true;
                }
            }

            return false;
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckNull(source);
            CheckNull(predicate);
            foreach (var s in source)
            {
                if (predicate(s))
                {
                    return s;
                }
            }

            throw new InvalidOperationException();
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            CheckNull(source);
            CheckNull(selector);
            foreach (var s in source)
            {
                yield return selector(s);
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            CheckNull(source);
            CheckNull(selector);
            foreach (var s in source)
            {
                foreach (var result in selector(s))
                {
                    yield return result;
                }
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckNull(source);
            CheckNull(predicate);
            foreach (var s in source)
            {
                if (predicate(s))
                {
                    yield return s;
                }
            }
        }

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
    this IEnumerable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TElement> elementSelector)
        {
            CheckNull(source);
            CheckNull(keySelector);
            CheckNull(elementSelector);
            Dictionary<TKey, TElement> result = new Dictionary<TKey, TElement>();

            foreach (var v in source)
            {
                result.Add(keySelector(v), elementSelector(v));
            }

            return result;
        }

        private static void CheckNull(object parameter)
        {
            _ = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }
    }
}
