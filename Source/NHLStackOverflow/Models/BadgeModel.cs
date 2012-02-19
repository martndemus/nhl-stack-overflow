using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Badge : IValidatableObject
    {
        // GUID
        public int BadgeID { get; set; }

        // Data
        public string Name { get; set; }

        // TimeStamps
        public DateTime Created_At { get; set; }

        // Relations
        public User User { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}