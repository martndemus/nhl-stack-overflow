﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Message
    {
        public Message()
        {
            this.Created_At = DateTime.Now.ToString();
            this.Viewed = 0;
        }
        // GUID
        [Required]
        public int MessageID { get; set; }

        // Data
        [Required]
        [MinLength(10)]
        [MaxLength(140)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        // Timestamps
        [Required]
        public string Created_At { get; set; }
        public string LastEdited { get; set; }

        [Required]
        [Range(0, 1)] // 0 = ongelezen, 1 = gelezen
        public int Viewed { get; set; }
        // Relations
        [Required]
        public int? SenderId { get; set; }
        [Required]
        public int? ReceiverId { get; set; }
        public int? QuestionId { get; set; }
    }
}