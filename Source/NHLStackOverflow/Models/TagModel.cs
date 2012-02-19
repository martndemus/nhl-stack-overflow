using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Tag : IValidatableObject
    {
        // GUID
        public int TagID { get; set; }

        // Data
        public string Beschrijving { get; set; }

        // Relations
        public ICollection<QuestionTag> Tags { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}