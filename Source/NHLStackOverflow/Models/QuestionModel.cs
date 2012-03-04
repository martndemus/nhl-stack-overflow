using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Question
    {
        public Question()
        {
            this.Created_At = DateTime.Now.ToString();
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
        public int Answered { get; set; }
        public int Flag { get; set; }

        // Timestamps
        [Required]
        public string Created_At { get; set; }
        public string LastEdited { get; set; }

        // Relations
        [Required]
        public User User { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<QuestionTag> Tags { get; set; }
    }
}