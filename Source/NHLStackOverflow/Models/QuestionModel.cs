using System;
using System.ComponentModel.DataAnnotations;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Models
{
    public class Question
    {
        public Question()
        {
            this.Created_At = StringToDateTime.ToUnixTimeStamp(DateTime.Now);
        }

        // GUID
        public int QuestionID { get; set; }

        // Data
        [Required]
        [MinLength(10)]
        [MaxLength(140)]
        public string Title { get; set; }

        [Required]
        [MinLength(140)]
        public string Content { get; set; }

        public int Votes { get; set; }
        public int Views { get; set; }

        [Range(0,1)]
        public int Answered { get; set; }
        [Range(0,1)]
        public int Flag { get; set; }

        // Timestamps
        [Required]
        public double Created_At { get; set; }
        public double? LastEdited { get; set; }

        // Relations
        [Required]
        public int? UserId { get; set; }
    }
}