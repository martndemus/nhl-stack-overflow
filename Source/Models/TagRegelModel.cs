using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class TagRegel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public Question Question { get; set; }

        [Required]
        public Tag Tag { get; set; }

    }
}