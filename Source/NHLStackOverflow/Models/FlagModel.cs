using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Flag
    {
        public int FlagID { get; set; }

        public int QuestionID { get; set; }
        public int CommentID { get; set; }
        public int AnswerID { get; set; }
        
        [Required]
        public int UserID { get; set; }

    }
}