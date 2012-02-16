using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Models
{
    public class Read
    {
        [Required]
        [Key]
        public User User { get; set; }

        [Required]
        [Key]
        public Question Question { get; set; }
    }
}