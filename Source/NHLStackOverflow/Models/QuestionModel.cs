using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace NHLStackOverflow.Models
{
    public class Question
    {
        // GUID
        public int QuestionID { get; set; }

        // Data
        public string Title { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
        public int Answered { get; set; }
        public int Flag { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; }
        public DateTime LastEdited { get; set; }

        // Relations
        public User User { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<QuestionTag> Tags { get; set; }
    }
}