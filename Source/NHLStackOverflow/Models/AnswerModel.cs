using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace NHLStackOverflow.Models
{
    public class Answer
    {
        // GUID
        public int AnswerID { get; set; }

        // Data
        public string Content { get; set; }
        public int Votes { get; set; }
        public int Flag { get; set; }

        // TimeStamps
        public DateTime Date { get; set; }
        public DateTime LastEdited { get; set; }

        // Relations
        public User User { get; set; }
        public Question Question { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}