using System.Collections.Generic;
using System.Linq;

namespace NHLStackOverflow.Classes
{
    /// <summary>
    /// A class that will trim all unneeded words so we can do a better search
    /// </summary>
    public static class StringFilter
    {
        /// <summary>
        /// A method that will trim all unneeded words
        /// </summary>
        /// <param name="ToTrim">A string (sentence) which needs to be stripped of unnneeded words</param>
        /// <returns>A string array with all the "good" words</returns>
        public static string[] Trim(string ToTrim)
        {
            // the list of all the unneeded words
            string[] TrimAgaist = new string[14] { "hoe", "wat", "wanneer", "de", "het", "een", "help", "hulp", "nodig", "taal", "maak", "je", "doe", "in" };
            // the new array with all the words of the sentence (words are separated by a space)
            string[] temp = ToTrim.Split(' ');
            // we need a dynamic list where we can store strings
            List<string> TrimArray = new List<string>();
            // put all the strings in this array (so we can delete them)
            foreach(string a in temp)
                TrimArray.Add(a);
            // now loop through all the string in this array and check if there are some which should be deleted
            for (int i = 0; i < TrimArray.Count(); i++ )
            {
                if (TrimAgaist.Contains(TrimArray[i]))
                {
                    // the string was found :< sorry bro but I have to delete this one :<
                    TrimArray.RemoveAt(i);
                    i--; // to prevent jumping over
                }
            }
            // build the output string
            string[] output = new string[TrimArray.Count()];
            for (int i = 0; i < TrimArray.Count(); i++)
                output[i] = TrimArray[i];
            return output;
        }
    }
}