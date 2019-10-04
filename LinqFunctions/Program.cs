using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqFunctions
{
    class Program
    {
        static void Main()
        {
            int[] numbers = { 1, 3, 4, 5, 9, 22, 16 };

            Func<int, int> selector = x => x / 2;

            foreach (var r in petOwners.SelectMany(x => selector(x)).Where(z => z.StartsWith("S")))
            {
                Console.WriteLine(r);
            }
        }
    }
}
