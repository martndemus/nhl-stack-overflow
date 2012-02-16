using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace NHLStackOverflow.Models
{
    public class QuestionTag
    {
        // GUID
        public int QuestionTagID { get; set; }

        // Relations
        public Question Question { get; set; }
        public Tag Tag { get; set; }
    }
}