using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace NHLStackOverflow.Models
{
    public class Read : IValidatableObject
    {
        // GUID
        public int ReadID { get; set; }

        // Relations
        [Required]
        public User User { get; set; }
        [Required]
        public Question Question { get; set; }
    }
}