using System;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class VoteUser
    {
        [Required]
        public int VoteUserID { get; set; } 

        [Required]
        public int UserID { get; set; }

        public int QuestionID { get; set; }
        public int AnswerID { get; set; }

    }
}