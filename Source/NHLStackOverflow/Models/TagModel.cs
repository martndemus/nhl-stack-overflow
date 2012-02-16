using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace NHLStackOverflow.Models
{
    public class Tag
    {
        // GUID
        public int TagID { get; set; }

        // Data
        public string Beschrijving { get; set; }

        // Relations
        public ICollection<QuestionTag> Tags { get; set; }
    }
}