﻿using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Option
    {
        // GUID
        public int OptionID { get; set; }

        // Data
        [Required]
        [Key]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
    }
}