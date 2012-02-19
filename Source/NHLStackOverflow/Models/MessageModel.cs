using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Message : IValidatableObject
    {
        // GUID
        public int MessageID { get; set; }

        // Data
        public string Title { get; set; }
        public string Content { get; set; }

        // Timestamps
        public DateTime Created_At { get; set; }
        public DateTime LastEdited { get; set; }

        // Relations
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public Question Post { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}