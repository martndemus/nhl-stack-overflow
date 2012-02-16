using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class Favorite
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Question Post { get; set; }
    }
}