using System;
using System.Collections.Generic;

namespace LinqFunctions
{
    public static class Functions
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            bool result = false;
            if (source == null || predicate == null)
            {
                throw new ArgumentException("Source or nameof(predicate) cannot be null");
            }

            foreach (var s in source)
            {
                result = predicate(s);
            }

            return result;
        }
    }
}
