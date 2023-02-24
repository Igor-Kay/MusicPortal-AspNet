using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        public static List<T> removeDuplicates<T>(List<T> list)
        {
            return new HashSet<T>(list).ToList();
        }
        static void Main()
        {
            List<int> list = new List<int>();
            string[] line = (Console.ReadLine().Split(" "));
            foreach (string s in line)
            {
                list.Add(int.Parse(s));
            }
            List<int> distinct = removeDuplicates(list);

            Console.WriteLine(String.Join(" ", distinct));
        }
    }
}
