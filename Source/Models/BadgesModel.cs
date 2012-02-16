﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class BadgesModel
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