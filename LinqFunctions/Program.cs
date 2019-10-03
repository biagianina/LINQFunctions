using System;
using System.Collections.Generic;

namespace LinqFunctions
{
    class Program
    {
        static void Main()
        {
            List<string> words = new List<string>();
            Func<string, bool> isEqual = null;
            var result = words.All(word => isEqual(word));
            Console.WriteLine(result);
        }
    }
}
