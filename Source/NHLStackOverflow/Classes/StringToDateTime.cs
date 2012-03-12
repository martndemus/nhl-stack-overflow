using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHLStackOverflow.Classes
{
    public static class StringToDateTime
    {
        /// <summary>
        /// A method which does the invert of DateTime.ToString()
        /// </summary>
        /// <param name="invoer">Expects a string in the form: day-month-year hour:minutes:seconds
        /// or simpely DateTime.Now</param>
        /// <returns></returns>
        public static DateTime toDateTime(string invoer)
        {
            // the separators in our string
            char[] separators = new char[] { ' ', '-', ':' };
            string[] separated = new string[6]; // will contain the individual parts of the datum
            separated = invoer.Split(separators);

            int[] separatedTotaly = new int[6]; // will conatin the individual parts of the datum as an int
            for (int i = 0; i < 6; i++) // for converting em
            {
                separatedTotaly[i] = Convert.ToInt32(separated[i]);
            }

            // create a new DateTime object of the given input string
            DateTime datum = new DateTime(separatedTotaly[2], separatedTotaly[1], separatedTotaly[0], separatedTotaly[3],
                separatedTotaly[4], separatedTotaly[5]);

            return datum;
        }

        public static string toSmootherTime(DateTime dt)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 60)
            {
                return ts.Seconds == 1 ? "één seconde" : ts.Seconds + " seconden";
            }
            if (delta < 120)
            {
                return "één minuut";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minuten";
            }
            if (delta < 5400) // 90 * 60
            {
                return "één uur";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " uur";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "gisteren";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " dagen";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "één maand" : months + " maanden";
            }
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "één jaar" : years + " jaar";
        }
    }
}