using MusicPortal.DAL.Enum;
using System;
using System.Collections.Generic;
using Utils;

namespace MusicPotral.testHueti
{
    internal class Program
    {
        static string Filter(string str, List<char> charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), " ");
            }

            return str;
        }
        static void Main(string[] args)
        {
                List<string> list = FilterGenre.returnGenre();
                foreach (string str in list) {
                Console.WriteLine(str);
            }
        }
    }
}
