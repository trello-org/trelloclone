using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            foreach (var nr in GetNumbersGreaterThan3(new List<int> { 1, 2, 3, 4, 5 })) {
                Console.WriteLine(nr);
			}
            foreach (var nr in GetNumbersGreaterThan3Yield(new List<int> { 1, 2, 3, 4, 5 }))
            {
                Console.WriteLine(nr);
            }
        }
    

        public static IEnumerable<int> GetNumbersGreaterThan3(List<int> numbers)
        {
            var theNumbers = new List<int>();
            foreach (var nr in numbers)
            {
                if (nr > 3)
                    theNumbers.Add(nr);
            }
            return theNumbers;
        }

        public static IEnumerable<int> GetNumbersGreaterThan3Yield(List<int> numbers)
        {
            foreach (var nr in numbers)
            {
                if (nr > 3)
                    yield return nr;
            }
        }
    }
}
