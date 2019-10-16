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
            var intComparer = new MyComparer<int>((x, y) => x.CompareTo(y));
            Func<Customer, int> ageSelector = c => c.Age;
            var firstComparer = new KeyComparer<Customer, int>(intComparer, ageSelector);
            var stringComparer = new MyComparer<string>((x, y) => x.CompareTo(y));
            Func<Customer, string> nameSelector = c => c.Name;
            var secondComparer = new KeyComparer<Customer, string>(stringComparer, nameSelector);
            foreach (var item in customers.OrderBy(customer => customer, firstComparer).ThenBy(customers => customers, secondComparer))
            {
                Console.WriteLine(item.Age + " " + item.Name);
            }
        }
    }
}