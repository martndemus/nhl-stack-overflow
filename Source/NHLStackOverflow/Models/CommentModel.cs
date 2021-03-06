﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Models
{
    public class Comment //: IValidatableObject
    {
        public Comment()
        {
            this.Votes = 0;
            this.Created_At = StringToDateTime.ToUnixTimeStamp(DateTime.Now);
        }
        // GUID
        [Required]
        public int CommentID { get; set; }

        // Data
        [Required]
        [MinLength(10)]
        public string Content { get; set; }
        public int Votes { get; set; }
        public int Flag { get; set; } // can be set by people to mark it as a bad comment

        // TimeStamps
        [Required]
        public double Created_At { get; set; }
        public double? LastEdited { get; set; }

        // Relations
        [Required]
        public int? UserId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if ((QuestionId != null && AnswerId != null) || (QuestionId == 0 && AnswerId == 0))
        //    {
        //        yield return new ValidationResult("");
        //    }
        //}
    }
}