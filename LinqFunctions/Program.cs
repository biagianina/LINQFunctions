using System;

namespace LinqFunctions
{
    class Program
    {
        static void Main()
        {
            Customer sam = new Customer { Age = 25, Name = "Sam", ItemsInCart = 2, State = "Albania" };
            Customer dave = new Customer { Age = 26, Name = "Dave", ItemsInCart = 2, State = "Romania" };
            Customer julia = new Customer { Age = 25, Name = "Julia", ItemsInCart = 4, State = "Greece" };
            Customer sue = new Customer { Age = 28, Name = "Sue", ItemsInCart = 1, State = "Italy" };
            Customer sally = new Customer { Age = 21, Name = "Sally", ItemsInCart = 3, State = "Malta" };
            Customer adam = new Customer { Age = 25, Name = "Adam", ItemsInCart = 5, State = "Denmark" };
            Customer angie = new Customer { Age = 19, Name = "Angie", ItemsInCart = 2, State = "France" };
            var customers = new[]
            {
            sam,
            dave,
            julia,
            sue,
            sally,
            adam,
            angie
            };
            var intComparer = new MyComparer<int>((x, y) => x.CompareTo(y));
            Func<Customer, int> ageSelector = c => c.Age;
            var firstComparer = new KeyComparer<Customer, int>(intComparer, ageSelector);
            var stringComparer = new MyComparer<string>((x, y) => x.CompareTo(y));
            Func<Customer, string> nameSelector = c => c.Name;
            var secondComparer = new KeyComparer<Customer, string>(stringComparer, nameSelector);
            Func<Customer, int> productSelector = c => c.ItemsInCart;
            var thirdComparer = new KeyComparer<Customer, int>(intComparer, productSelector);
            Func<Customer, string> stateSelector = c => c.State;
            var fourthComparer = new KeyComparer<Customer, string>(stringComparer, stateSelector);
            var orderByAge = customers.OrderBy(customer => customer, firstComparer);
            var orderByName = orderByAge.ThenBy(customer => customer, secondComparer);
            var orderByItemsInCart = orderByName.ThenBy(customer => customer, thirdComparer);
            var orderByState = orderByItemsInCart.ThenBy(customer => customer, fourthComparer);

            Console.WriteLine("Customers ordered by age: ");
            foreach (var item in orderByAge)
            {
                Console.WriteLine(item.Age + " " + item.Name + " " + item.ItemsInCart + " " + item.State);
            }

            Console.WriteLine("Customers then ordered by name: ");
            foreach (var item in orderByName)
            {
                Console.WriteLine(item.Age + " " + item.Name + " " + item.ItemsInCart + " " + item.State);
            }

            Console.WriteLine("Customers then ordered by items in cart: ");
            foreach (var item in orderByItemsInCart)
            {
                Console.WriteLine(item.Age + " " + item.Name + " " + item.ItemsInCart + " " + item.State);
            }

            Console.WriteLine("Customers then ordered by state: ");
            foreach (var item in orderByState)
            {
                Console.WriteLine(item.Age + " " + item.Name + " " + item.ItemsInCart + " " + item.State);
            }
        }
    }
}