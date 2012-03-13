using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models.FormDataModels
{
    public class Account
    {
        [RegularExpression(@"^[A-Z][\sa-zA-Z]+[a-zA-Z]$", ErrorMessage="De naam bevat niet toegestaande characters.")]
        public string Name { get; set; }

        public string Birthday { get; set; }

        [RegularExpression(@"^[A-Z][\sa-zA-Z]+[a-zA-Z]$", ErrorMessage="De plaats naam bevat niet toegestaande characters.")]
        public string Location { get; set; }

        [RegularExpression(@"^http(s)?:\/\/", ErrorMessage = "De website-url bevat niet toegestaande characters.")]
        public string Website { get; set; }

        public string Languages { get; set; }
    }

    public class PassEmail
    {
        public string NowPassword { get; set; }

        public string Password1 { get; set; }

        public string Password2 { get; set; }

        [RegularExpression(@"^[0-9A-Za-z._%+-]+@[0-9A-Za-z.-]+\.[A-Za-z]{2,64}$", ErrorMessage="Het nieuwe email-adres is onjuist.")]
        public string Email { get; set; }

    }
}