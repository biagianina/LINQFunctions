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
            Assert.True(numbers.All(isEqual));
        }

        [Fact]
        public void AllFalse()
        {
            List<int> numbers = new List<int> { 1, 1, 1, 4 };
            Func<int, bool> isEqual = number => number == 1;
            Assert.False(numbers.All(isEqual));
        }
    }
}
