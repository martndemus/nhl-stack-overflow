using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class QuestionTag
    {
        // GUID
        public int QuestionTagID { get; set; }

        // Relations
        [Required]
        public Question Question { get; set; }
        [Required]
        public Tag Tag { get; set; }
    }
}