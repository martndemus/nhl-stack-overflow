using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class User : IValidatableObject
    {
        // Init default values
        public User()
        {
            this.Created_At = DateTime.Now.ToString();
            this.LastOnline = this.Created_At;
            this.Rank = 0;
        }
        // GUID
        public int UserID { get; set; }

        // Data
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(28)]
        [RegularExpression(@"^[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+/]{28}$")]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Rank { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string Languages { get; set; }

        // Timestamps
        [Required]
        public string Created_At { get; set; }
        public string LastOnline { get; set; }

        // Relations
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Badge> Badges { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Read> Read { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
