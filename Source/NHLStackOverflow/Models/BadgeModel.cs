using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace NHLStackOverflow.Models
{
    public class Badge
    {
        // GUID
        public int BadgeID { get; set; }

        // Data
        public string Name { get; set; }

        // TimeStamps
        public DateTime Date { get; set; }

        // Relations
        public User User { get; set; }        
    }
}