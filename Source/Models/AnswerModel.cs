using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class Answer
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [Key]
        public User User { get; set; }

        [Required]
        [Key]
        public Question Question { get; set; }
        
        [Required]
        public int Votes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastEdited { get; set; }

        [Required]
        public int Flag { get; set; }

    }
}