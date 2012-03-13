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
}