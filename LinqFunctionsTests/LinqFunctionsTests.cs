using System;
using System.Collections.Generic;
using Xunit;

namespace LinqFunctions
{
    public class LinqFunctionsTests
    {
        [Fact]
        public void AllTrue()
        {
            List<int> numbers = new List<int> { 1, 1, 1, 1 };
            Func<int, bool> isEqual = number => number == 1;
            Assert.True(numbers.All(number => isEqual(number)));
        }

        [Fact]
        public void AllFalse()
        {
            List<int> numbers = new List<int> { 1, 1, 1, 4 };
            Func<int, bool> isEqual = number => number == 1;
            Assert.False(numbers.All(number => isEqual(number)));
        }

        [Fact]
        public void AnyTrue()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4 };
            Func<int, bool> isEqual = number => number == 3;
            Assert.True(numbers.Any(number => isEqual(number)));
        }

        [Fact]
        public void AnyFalse()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4 };
            Func<int, bool> isEqual = number => number == 5;
            Assert.False(numbers.Any(number => isEqual(number)));
        }

        [Fact]
        public void NullArgument()
        {
            List<string> words = null;
            Func<string, bool> isEqual = null;
            Assert.Throws<ArgumentNullException>(() => words.All(word => isEqual(word)));
        }

        [Fact]
        public void First()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 2 };
            Func<int, bool> isEqual = number => number == 2;
            Assert.Equal(2, numbers.First(number => isEqual(number)));
        }

        [Fact]
        public void FirstException()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 2 };
            Func<int, bool> isEqual = number => number == 5;
            Assert.Throws<InvalidOperationException>(() => numbers.First(number => isEqual(number)));
        }

        [Fact]
        public void Select()
        {
            List<string> words = new List<string> { "a", "abc", "ab", "abcd" };
            Func<string, string> acceptWord = word => word.Substring(1);
            IEnumerable<string> expected = new List<string> { "", "bc", "b", "bcd" };
            Assert.Equal(expected, words.Select(word => acceptWord(word)));
        }

        [Fact]
        public void SelectMany()
        {
            PetOwner[] petOwners =
            {
                new PetOwner { Name = "Higa", Pets = new List<string> { "Scruffy", "Sam" } },
                new PetOwner { Name = "Ashkenazi", Pets = new List<string> { "Walker", "Sugar" } },
                new PetOwner { Name = "Price", Pets = new List<string> { "Scratches", "Diesel" } },
                new PetOwner { Name = "Hines", Pets = new List<string> { "Dusty" } }
            };
            Func<PetOwner, List<string>> selector = petOwner => petOwner.Pets;
            IEnumerable<string> expected = new List<string> { "Scruffy", "Sam", "Walker", "Sugar", "Scratches", "Diesel", "Dusty" };
            Assert.Equal(expected, petOwners.SelectMany(petOwner => selector(petOwner)));
        }

        [Fact]
        public void Where()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 7, 8, 10, 11, 22, 33, 34 };
            Func<int, bool> isEven = number => number % 2 == 0;
            int[] expected = { 2, 4, 8, 10, 22, 34 };
            Assert.Equal(expected, numbers.Where(number => isEven(number)));
        }

        [Fact]
        public void ToDictionary()
        {
            PetOwner[] petOwners =
            {
                new PetOwner { Name = "Higa", Pets = new List<string> { "Scruffy", "Sam" } },
                new PetOwner { Name = "Ashkenazi", Pets = new List<string> { "Walker", "Sugar" } },
                new PetOwner { Name = "Price", Pets = new List<string> { "Scratches", "Diesel" } },
                new PetOwner { Name = "Hines", Pets = new List<string> { "Dusty" } }
            };

            Dictionary<string, int> expected = new Dictionary<string, int>
            {
                { "Higa", 2 },
                { "Ashkenazi", 2 },
                { "Price", 2 },
                { "Hines", 1 }
            };
            Func<PetOwner, string> owner = petOwner => petOwner.Name;
            Func<PetOwner, int> numberOfPets = petOwner => petOwner.Pets.Count;
            Assert.Equal(expected, petOwners.ToDictionary(petOwner => owner(petOwner), petOwner => numberOfPets(petOwner)));
        }

        [Fact]
        public void Zip()
        {
            int[] nr = { 1, 2, 3, 4 };
            string[] numbers = { "one", "two", "three" };
            Func<int, string, string> selector = (nr, number) => nr + " " + number;
            IEnumerable<string> expected = new List<string> { "1 one", "2 two", "3 three" };
            Assert.Equal(expected, nr.Zip(numbers, (n1, n2) => selector(n1, n2)));
        }

        [Fact]
        public void Aggregate()
        {
            string[] fruits = { "banana", "mango", "orange", "apple", "passionfruit" };
            const string seed = "grape";
            Func<string, string, string> longestFruit = (seed, fruit) => fruit.Length > seed.Length ? fruit : seed;
            Assert.Equal("passionfruit", fruits.Aggregate(seed, longestFruit));
        }

        [Fact]
        public void Join()
        {
            int[] first = { 4, 3, 0 };
            int[] second = { 5, 4, 2 };
            Func<int, int> firstKey = number => number + 1;
            Func<int, int> secondKey = number => number;
            Func<int, int, int> selector = (x, y) => x;
            IEnumerable<int> expected = new List<int> { 4, 3 };
            Assert.Equal(expected, first.Join(second, firstKey, secondKey, selector));
        }

        [Fact]
        public void Distinct()
        {
            List<int> ages = new List<int> { 21, 46, 46, 55, 17, 21, 55, 55 };
            IEnumerable<int> expected = new List<int> { 21, 46, 55, 17 };
            var comparer = new MyComparer<int>();
            Assert.Equal(expected, ages.Distinct(comparer));
        }

        [Fact]
        public void Union()
        {
            List<int> ints1 = new List<int> { 5, 3, 9, 7, 5, 9, 3, 7 };
            List<int> ints2 = new List<int> { 8, 3, 6, 4, 4, 9, 1, 0 };
            IEnumerable<int> expected = new List<int> { 5, 3, 9, 7, 8, 6, 4, 1, 0 };
            var comparer = new MyComparer<int>();
            Assert.Equal(expected, ints1.Union(ints2, comparer));
        }

        [Fact]
        public void Intersect()
        {
            List<int> ints1 = new List<int> { 5, 3, 9, 7, 5, 9, 3, 7 };
            List<int> ints2 = new List<int> { 8, 3, 6, 4, 4, 9, 1, 0 };
            IEnumerable<int> expected = new List<int> { 3, 9 };
            var comparer = new MyComparer<int>();
            Assert.Equal(expected, ints1.Intersect(ints2, comparer));
        }

        [Fact]
        public void Except()
        {
            List<int> ints1 = new List<int> { 5, 3, 9, 7, 5, 9, 3, 7 };
            List<int> ints2 = new List<int> { 8, 3, 6, 4, 4, 9, 1, 0 };
            IEnumerable<int> expected = new List<int> { 5, 7, 5, 7 };
            var comparer = new MyComparer<int>();
            Assert.Equal(expected, ints1.Except(ints2, comparer));
        }

        [Fact]
        public void GroupBy()
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
            List<string> expected = new List<string> { "25 Sam Julia", "26 Dave", "28 Sue" };
            Assert.Equal(expected, customers.GroupBy(key, element, selector, comparer));
        }
    }
}
