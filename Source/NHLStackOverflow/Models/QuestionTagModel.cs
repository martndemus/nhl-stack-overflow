using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class QuestionTag : IValidatableObject
    {
        // GUID
        public int QuestionTagID { get; set; }

        // Relations
        public Question Question { get; set; }
        public Tag Tag { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}