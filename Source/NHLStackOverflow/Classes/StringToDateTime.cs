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

        /// <summary>
        /// A method which will make an string (so many days ago) from an unix timestamp
        /// </summary>
        /// <param name="date">The unix timestamp</param>
        /// <returns>A string containing how many minutes, seconds, hours, days, months, years ago it was made</returns>
        public static string ConvertToUnixTimestamp(double date)
        {
            // get the origin time (of unix timestamps)
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            // get the difference since then
            TimeSpan since = DateTime.Now - origin;
            // the get the diffrene with the given unix timestamp
            double delta = Math.Abs(since.TotalSeconds - date);

            // then check the value and create a string of it
            if (delta < 60)
            {
                return delta == 1 ? "één seconde" : (int)delta + " seconden";
            }
            if (delta < 120)
            {
                return "één minuut";
            }
            if (delta < 2700) // 45 * 60
            {
                return (int)(delta / 60) + " minuten";
            }
            if (delta < 5400) // 90 * 60
            {
                return "één uur";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return (int)(delta / 60 / 60) + " uur";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "één dag";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return (int)(delta / 60 / 60 / 24) + " dagen";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Abs((double)(delta / 60 / 60 / 24) / 30));
                return months <= 1 ? "één maand" : months + " maanden";
            }
            // if we got here it's in the size of a year so get the amount of years
            int years = Convert.ToInt32(Math.Abs((double)(delta / 60 / 60 / 24) / 365));
            // and return the amount of years
            return years <= 1 ? "één jaar" : years + " jaar";
        }

        /// <summary>
        /// A method which takes dateTime and  returns the unix timestamp of it
        /// </summary>
        /// <param name="date">A dateTime which needs to be converted</param>
        /// <returns>A double with the amount of seconds since the unix time</returns>
        public static double ToUnixTimeStamp(DateTime date)
        {
            // get the origin datetime
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            // and then calculate the difference in seconds since that time with the given date
            TimeSpan diff = date - origin;
            // and then return the abs of it
            return Math.Floor(diff.TotalSeconds);
        }
    }
}