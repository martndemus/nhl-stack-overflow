using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class User
    {
        [Required]
        [Key] // <- Unique key!
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Username moet tussen 6 en 20 karakters lang zijn.")]
        [Display(Name = "User Nickname")]
        [RegularExpression(@"^\w[\w\d]+$", ErrorMessage = "Geen spaties, alleen letters en cijfers, moet beginnen met een letter.")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$")]
        [Display(Name = "User E-mail Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Date Account Created")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [Required]
        [Display(Name = "Date Last Online")]
        [DataType(DataType.Date)]
        public DateTime LastOnline { get; set; }

        [Required]
        [Display(Name = "User Rank")]
        public int Rank { get; set; }

        [Display(Name = "User Real Name")]
        public string Name { get; set; }

        [Range(15, 99)]
        [RegularExpression(@"\d{1,2}$", ErrorMessage = "Graag je leeftijd in cijfers!")]
        [Display(Name = "User Age")]
        public int Age { get; set; }

        [Display(Name = "User Location")]
        public string Location { get; set; }

        [Display(Name = "User Website Url")]
        public string Website { get; set; }

        [Display(Name = "User Programming Languages")]
        public string Languages { get; set; }

        [Display(Name = "User Badges")]
        public string Badges { get; set; }               
    }
}