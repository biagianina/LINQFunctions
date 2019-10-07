using System;

namespace LinqFunctions
{
    class Program
    {
        static void Main()
        {
            // Example customers.
            var customers = new[]
            {
            new Customer { ID = 5, Name = "Sam" },
            new Customer { ID = 6, Name = "Dave" },
            new Customer { ID = 7, Name = "Julia" },
            new Customer { ID = 8, Name = "Sue" }
            };

            // Example orders.
            var orders = new[]
            {
            new Order { ID = 5, Product = "Book" },
            new Order { ID = 6, Product = "Game" },
            new Order { ID = 7, Product = "Computer" },
            new Order { ID = 8, Product = "Shirt" }
            };

            // Join on the ID properties.
            var query = from c in customers
                        join o in orders on c.ID equals o.ID
                        select new { c.Name, o.Product };

            // Display joined groups.
            foreach (var group in query)
            {
                Console.WriteLine("{0} bought {1}", group.Name, group.Product);
            }
        }
    }
}