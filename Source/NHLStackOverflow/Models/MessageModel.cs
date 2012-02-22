using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Message : IValidatableObject
    {
        public Message()
        {
            this.Created_At = DateTime.Now.ToString();
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
        [MinLength(50)]
        public string Content { get; set; }

        // Timestamps
        [Required]
        public string Created_At { get; set; }
        public string LastEdited { get; set; }

        // Relations
        [Required]
        public User Sender { get; set; }
        [Required]
        public User Receiver { get; set; }
        public Question Post { get; set; }
    }
}