using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace NHLStackOverflow.Models
{
    public class Read : IValidatableObject
    {
        // GUID
        public int ReadID { get; set; }

        // Relations
        public User User { get; set; }
        public Question Question { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}