using System
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{mbox;
    public class Comment
    {m
        [Required]
        [Key] // <- Unique key!
        public int Id { get; set; }

        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public string Created { get; set; }
        public string

    }
}