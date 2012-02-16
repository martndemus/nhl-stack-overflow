using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using NHLStackOverflow.Models;
using System;

namespace NHLStackOverflow.Models
{
    public class Answer
    {
        [Required]
        [Key]
        public User User { get; set; }

        [Required]
        [Key]
        public Question Question { get; set; }

        [Required]
        [Key]
        public int AsnwerID { get; set; }

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