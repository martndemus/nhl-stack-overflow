using System;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class User
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
        [Required(ErrorMessage="De naam is verplicht.")]
        [MinLength(5)]
        [RegularExpression(@"^[a-zA-Z][\w\d]+$")] // Must start with a letter, then any number of word chars (a-z + _) and digits
        public string UserName { get; set; }

        [Required(ErrorMessage="Een wachtwoord is verplicht.")]
        [RegularExpression(@"^[abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+\/=]+$")] // Must consist of Base64 characters
        public string Password { get; set; }

        [Required(ErrorMessage="Een e-mail is verplicht.")]
        [RegularExpression(@"^[0-9A-Za-z._%+-]+@[0-9A-Za-z.-]+\.[A-Za-z]{2,64}$")]
        public string Email { get; set; }
        
        [Required]
        public int Rank { get; set; }

        [RegularExpression(@"^[A-Z][\sa-zA-Z]+[a-zA-Z]$")]
        public string Name { get; set; }

        public string Birthday { get; set; }

        [RegularExpression(@"^[A-Z][\sa-zA-Z]+[a-zA-Z]$")]
        public string Location { get; set; }

        [RegularExpression(@"^http(s)?:\/\/")]
        public string Website { get; set; }

        public string Languages { get; set; }

        // Timestamps
        [Required]
        public string Created_At { get; set; }
        public string LastOnline { get; set; }
    }

}
