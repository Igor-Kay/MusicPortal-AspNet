using MusicPortal.DAL.Enum;
using System;
using System.Collections.Generic;

namespace Utils
{
    public class FilterGenre
    {
        public static string Filter(string str, List<char> charsToRemove)
        {
            foreach (char c in charsToRemove)
            {
                str = str.Replace(c.ToString(), " ");
            }

            return str;
        }

        public static List<string> returnGenre()
        {

            List<char> charsToRemove = new List<char>() { '@', '_', ',', '.' };
            List<string> charsToAdd = new List<string>();
            foreach (GenreEnum i in Enum.GetValues(typeof(GenreEnum)))
            {
                string str = i.ToString(Filter(i.ToString(), charsToRemove));
                charsToAdd.Add(str.ToString());

            }

            return charsToAdd;
        }


    }
}
