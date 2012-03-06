﻿using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Read
    {
        // GUID
        public int ReadID { get; set; }

        // Relations
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Question { get; set; }
        //[Required]
        //public User User { get; set; }
        //[Required]
        //public Question Question { get; set; }
    }
}