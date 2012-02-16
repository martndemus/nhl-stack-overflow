using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class Tag
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Beschrijving { get; set; }

    }
}