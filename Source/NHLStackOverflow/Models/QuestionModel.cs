﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Question : IValidatableObject
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
        public DateTime Created_At { get; set; }
        public DateTime LastEdited { get; set; }

        // Relations
        public User User { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<QuestionTag> Tags { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}