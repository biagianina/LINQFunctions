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
    }
}
