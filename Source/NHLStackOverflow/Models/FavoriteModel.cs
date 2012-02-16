using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace NHLStackOverflow.Models
{
    public class Favorite
    {
        // GUID
        public int FavoriteID { get; set; }

        // TimeStamp
        public DateTime Date { get; set; }

        // Relations
        public User User { get; set; }
        public Question Post { get; set; }
    }
}