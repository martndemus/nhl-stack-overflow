﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Answer : IValidatableObject
    {
        // GUID
        public int AnswerID { get; set; }

        // Data
        [Required]
        public string Content { get; set; }
        public int Votes { get; set; }

        [Range (0,1)]
        public int Flag { get; set; }

        // TimeStamps
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created_At { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastEdited { get; set; }

        // Relations
        public User User { get; set; }
        public Question Question { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var ValidationResults = new List<ValidationResult>();

            return ValidationResults;
        }
    }
}