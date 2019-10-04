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
    }
}
