using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class Favorite : IValidatableObject
    {
        public Favorite()
        {
            this.Created_At = DateTime.Now.ToString();
        }

        // GUID
        [Required]
        public int FavoriteID { get; set; }

        // TimeStamp
        [Required]
        public string Created_At { get; set; }

        // Relations
        [Required]
        public int UserId { get; set; }
        [Required]
        public int QuestionId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (UserId == 0 || QuestionId == 0)
            {
                yield return new ValidationResult("Missing relations to User && Question");
            }
        }
    }
}