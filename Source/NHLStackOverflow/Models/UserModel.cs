using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class User : IValidatableObject
    {
        // GUID
        public int UserID { get; set; }

        // Data
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string Languages { get; set; }

        // Timestamps
        public DateTime Created_At { get; set; }
        public DateTime LastOnline { get; set; }

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
