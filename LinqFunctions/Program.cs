using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqFunctions
{
    class Program
    {
        static void Main()
        {
            int[] nr = { 1, 2, 3, 4 };
            string[] numbers = { "one", "two", "three" };
            Func<int, string, string> selector = (nr, number) => nr + " " + number;
            foreach (var item in nr.Zip(numbers, selector))
            {
                Console.WriteLine(item);
            }
        }
    }
}
