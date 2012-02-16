using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Models
{
    /// <summary>
    /// Summary description for Question
    /// </summary>
    public class Question
    {
        [Required]
        [Key]
        public int Id { get; set; } 

        [Required]
        public User User { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Votes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastEdited { get; set; }

        [Required]
        public int Views { get; set; }

        [Required]
        public int Answered { get; set; }

        [Required]
        public int flag { get; set; }

    }
}