using System;

namespace LinqFunctions
{
    class Program
    {
        static void Main()
        {
            Customer sam = new Customer { Age = 25, Name = "Sam" };
            Customer dave = new Customer { Age = 26, Name = "Dave" };
            Customer julia = new Customer { Age = 25, Name = "Julia" };
            Customer sue = new Customer { Age = 28, Name = "Sue" };
            Customer sally = new Customer { Age = 21, Name = "Sally" };
            Customer adam = new Customer { Age = 25, Name = "Adam" };
            var customers = new[]
            {
            sam,
            dave,
            julia,
            sue,
            sally,
            adam
            };
            var comparer = new MyComparer<int>((x, y) => x.CompareTo(y));
            foreach (var item in customers.OrderBy(customer => customer.Age, comparer))
            {
                Console.WriteLine(item.Age + " " + item.Name);
            }
        }
    }
}