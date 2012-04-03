using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Classes
{
    /// <summary>
    /// A little class that holds a badge and the amount of badges that there are of that badge
    /// </summary>
    public class BadgeCount
    {
        public Badge badge;
        public int count;
    }
}