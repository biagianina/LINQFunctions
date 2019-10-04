using System;
using System.Collections.Generic;

namespace LinqFunctions
{
    public static class Functions
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckNullSourceOrPredicate(source, predicate);

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
            CheckNullSourceOrPredicate(source, predicate);
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
            CheckNullSourceOrPredicate(source, predicate);
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
            CheckNullSourceOrPredicate(source, selector);
            foreach (var s in source)
            {
                yield return selector(s);
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            CheckNullSourceOrPredicate(source, selector);
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
            CheckNullSourceOrPredicate(source, predicate);
            foreach (var s in source)
            {
                if (predicate(s))
                {
                    yield return s;
                }
            }
        }

        private static void CheckNullSourceOrPredicate(object source, object predicate)
        {
            _ = source ?? throw new ArgumentNullException(nameof(source));
            _ = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }
    }
}
