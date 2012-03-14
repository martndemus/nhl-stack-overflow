using System.Collections.Generic;
using System.Linq;

namespace NHLStackOverflow.Classes
{
    public static class StringFilter
    {
        public static string[] Trim(string ToTrim)
        {
            string[] TrimAgaist = new string[14] { "hoe", "wat", "wanneer", "de", "het", "een", "help", "hulp", "nodig", "taal", "maak", "je", "doe", "in" };
            string[] temp = ToTrim.Split(' ');
            List<string> TrimArray = new List<string>();
            foreach(string a in temp)
                TrimArray.Add(a);
            for (int i = 0; i < TrimArray.Count(); i++ )
            {
                if (TrimAgaist.Contains(TrimArray[i]))
                {
                    // the string was found :< sorry bro but I have to delete this one :<
                    TrimArray.RemoveAt(i);
                    i--; // to prevent jumping over
                }
            }
            string[] output = new string[TrimArray.Count()];
            for (int i = 0; i < TrimArray.Count(); i++)
                output[i] = TrimArray[i];
            return output;
        }
    }
}