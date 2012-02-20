using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Comment : IValidatableObject
    {
        // GUID
        public int CommentID { get; set; }

        // Data
        public string Content { get; set; }
        public int Votes { get; set; }

        // TimeStamps
        public string Created_At { get; set; }
        public string LastEdited { get; set; }

        // Relations
        public User User { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}