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

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
    this IEnumerable<TFirst> first,
    IEnumerable<TSecond> second,
    Func<TFirst, TSecond, TResult> resultSelector)
        {
            CheckNull(first);
            CheckNull(second);
            CheckNull(resultSelector);
            var f = first.GetEnumerator();
            var s = second.GetEnumerator();
            while (f.MoveNext() && s.MoveNext())
            {
                yield return resultSelector(f.Current, s.Current);
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
    this IEnumerable<TSource> source,
    TAccumulate seed,
    Func<TAccumulate, TSource, TAccumulate> func)
        {
            CheckNull(source);
            CheckNull(seed);
            CheckNull(func);
            var current = seed;
            foreach (var s in source)
            {
                current = func(current, s);
            }

            return current;
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
    this IEnumerable<TOuter> outer,
    IEnumerable<TInner> inner,
    Func<TOuter, TKey> outerKeySelector,
    Func<TInner, TKey> innerKeySelector,
    Func<TOuter, TInner, TResult> resultSelector)
        {
            CheckNull(outer);
            CheckNull(inner);
            CheckNull(outerKeySelector);
            CheckNull(innerKeySelector);
            CheckNull(resultSelector);
            foreach (var o in outer)
            {
                var outerKey = outerKeySelector(o);

                foreach (var i in inner)
                {
                    var innerKey = innerKeySelector(i);
                    if (outerKey.Equals(innerKey))
                    {
                        yield return resultSelector(o, i);
                    }
                }
            }
        }

        private static void CheckNull(object parameter)
        {
            _ = parameter ?? throw new ArgumentNullException(nameof(parameter));
        }
    }
}
