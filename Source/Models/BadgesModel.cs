using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class Badge
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Key]
        public User User { get; set; }

        [Required]
        public string Name { get; set; }
    }
}