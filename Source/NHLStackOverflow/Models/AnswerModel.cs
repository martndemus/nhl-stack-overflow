﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Answer
    {
        // Default values
        public Answer()
        {
            this.Created_At = DateTime.Now.ToString();
        }

        // GUID
        public int AnswerID { get; set; }

        // Data
        [Required]
        [MinLength(140)]
        public string Content { get; set; }

        public int Votes { get; set; }

        [Range (0,1)]
        public int Flag { get; set; }

        // TimeStamps
        [Required]
        public string Created_At { get; set; }
        public string LastEdited { get; set; }

        // Relations
        [Required]
        public int UserId { get; set; }
        [Required]
        public int QuestionId { get; set; }

        //[Required]
        //public User User { get; set; }
        //[Required]
        //public Question Question { get; set; }
    }
}