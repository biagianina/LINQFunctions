using System;
using System.Collections.Generic;

namespace LinqFunctions
{
    public static class Functions
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            CheckSource(source);
            CheckPredicate(predicate);
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
            CheckSource(source);
            CheckPredicate(predicate);
            foreach (var s in source)
            {
                if (predicate(s))
                {
                    return true;
                }
            }

            return false;
        }

        private static void CheckSource(object source)
        {
            if (source != null)
            {
                return;
            }

            throw new ArgumentNullException(nameof(source));
        }

        private static void CheckPredicate<TSource>(Func<TSource, bool> predicate)
        {
            if (predicate != null)
            {
                return;
            }

            throw new ArgumentNullException(nameof(predicate));
        }
    }
}
