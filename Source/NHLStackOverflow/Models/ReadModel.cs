using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
namespace NHLStackOverflow.Models
{
    public class Read
    {
        // GUID
        public int ReadID { get; set; }

        // Relations
        public User User { get; set; }
        public Question Question { get; set; }
    }
}