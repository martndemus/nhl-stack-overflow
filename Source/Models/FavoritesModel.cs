using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace NHLStackOverflow.Models
{
    public class Favorites
    {
        [Required]
        [Key]
        public User User { get; set; }

        [Required]
        [Key]
        public Question Post { get; set; }
    }
}