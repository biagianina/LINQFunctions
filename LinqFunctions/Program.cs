using System;

namespace LinqFunctions
{
    class Program
    {
        static void Main()
        {
            string[] fruits = { "banana", "mango", "orange", "apple", "passionfruit" };
            const string seed = "grape";
            Func<string, string, string> longestFruit = (seed, fruit) => fruit.Length > seed.Length ? fruit : seed;
            Console.WriteLine(fruits.Aggregate(seed, longestFruit));

            int[] ints = { 4, 8, 8, 3, 9, 0, 7, 8, 2 };
            const int total = 0;
            Func<int, int, int> totalEvens = (total, number) => number % 2 == 0 ? total + 1 : total;
            Console.WriteLine(ints.Aggregate(total, totalEvens));
        }
    }
}
