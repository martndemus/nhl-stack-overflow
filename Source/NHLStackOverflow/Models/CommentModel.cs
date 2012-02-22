using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Comment : IValidatableObject
    {
        public Comment()
        {
            this.Votes = 0;
            this.Created_At = DateTime.Now.ToString();
        }
        // GUID
        [Required]
        public int CommentID { get; set; }

        // Data
        [Required]
        [MinLength(50)]
        public string Content { get; set; }
        public int Votes { get; set; }

        // TimeStamps
        [Required]
        public string Created_At { get; set; }
        public string LastEdited { get; set; }

        // Relations
        [Required]
        public User User { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Question != null && Answer != null || Question == null && Answer == null)
            {
                yield return new ValidationResult("");
            }
        }
    }
}