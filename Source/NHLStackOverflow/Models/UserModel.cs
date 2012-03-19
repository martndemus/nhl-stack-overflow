using System;
using System.ComponentModel.DataAnnotations;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Models
{
    public class User
    {
        // Init default values
        public User()
        {
            this.Created_At = StringToDateTime.ToUnixTimeStamp(DateTime.Now);
            this.LastOnline = this.Created_At;
            this.Rank = 0;
            this.Activated = 0;
        }
        // GUID
        public int UserID { get; set; }

        // Data
        [Required(ErrorMessage="De naam is verplicht.")]
        [MinLength(5, ErrorMessage="Een username moet minstens 5 tekens lang zijn")]
        [RegularExpression(@"^[a-zA-Z][\w\d]+$", ErrorMessage="Een username mag alleen bestaan uit letters en cijfers")] // Must start with a letter, then any number of word chars (a-z + _) and digits
        public string UserName { get; set; }

        [Required(ErrorMessage="Een wachtwoord is verplicht.")]
        [RegularExpression(@"^[a-zA-Z0-9+\/=]+$", ErrorMessage="")] // Must consist of Base64 characters
        public string Password { get; set; }

        [Required(ErrorMessage="Een e-mail is verplicht.")]
        [RegularExpression(@"^[0-9A-Za-z._%+-]+@[0-9A-Za-z.-]+\.[A-Za-z]{2,64}$", ErrorMessage="Er is een ongeldig email adres ingevuld.")]
        public string Email { get; set; }
        
        [Required]
        public int Rank { get; set; }

        [Required]
        public int Activated { get; set; } // 0 if inactive, 1 if activated

        [RegularExpression(@"^[A-Z][\sa-zA-Z]+[a-zA-Z]$")]
        public string Name { get; set; }

        public string Birthday { get; set; }

        [RegularExpression(@"^[A-Z][\sa-zA-Z]+[a-zA-Z]$")]
        public string Location { get; set; }

        [RegularExpression(@"^http(s)?:\/\/")]
        public string Website { get; set; }

        public string Languages { get; set; }

        // gets set if the person uses the passLost page
        public string PassLost { get; set; }

        // Timestamps
        [Required]
        public double Created_At { get; set; }
        public double LastOnline { get; set; }
    }

}
