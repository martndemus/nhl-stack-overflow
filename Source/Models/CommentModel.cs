using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class Comment
    {
        [Required]
        [Key] // <- Unique key!
        public int Id { get; set; }

        public Question Question { get; set; }
        public Answer Answer { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LastOnline { get; set; }

    }
}