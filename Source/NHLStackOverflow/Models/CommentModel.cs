using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace NHLStackOverflow.Models
{
    public class Comment
    {
        // GUID
        public int CommentID { get; set; }

        // Data
        public string Content { get; set; }
        public int Votes { get; set; }

        // TimeStamps
        public DateTime Date { get; set; }
        public DateTime LastEdited { get; set; }

        // Relations
        public User User { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}