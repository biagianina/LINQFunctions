using System;
using System.Collections.Generic;

namespace LinqFunctions
{
    class Program
    {
        static void Main()
        {
            var customers = new[]
            {
            new Customer { Age = 25, Name = "Sam" },
            new Customer { Age = 26, Name = "Dave" },
            new Customer { Age = 25, Name = "Julia" },
            new Customer { Age = 28, Name = "Sue" }
            };
            var comparer = new MyComparer<int>();
            Func<Customer, int> key = customer => customer.Age;
            Func<Customer, string> element = customer => customer.Name;
            Func<int, IEnumerable<string>, string> selector = (key, elements) => key + " " + string.Join(" ", elements);
            foreach (var item in customers.GroupBy(key, element, selector, comparer))
            {
                Console.WriteLine(item);
            }
        }
    }
}