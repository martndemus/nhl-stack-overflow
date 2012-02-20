using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Favorite : IValidatableObject
    {
        // GUID
        public int FavoriteID { get; set; }

        // TimeStamp
        public string Created_At { get; set; }

        // Relations
        public User User { get; set; }
        public Question Post { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}