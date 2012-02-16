using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Models
{
    public class TagRegel
    {
        [Required]
        [Key]
        public Question Question { get; set; }

        [Required]
        [Key]
        public Tags Tag { get; set; }

    }
}