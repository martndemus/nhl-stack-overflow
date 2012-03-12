using System;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Message
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
        [MinLength(10)]
        public string Content { get; set; }

        // Timestamps
        [Required]
        public string Created_At { get; set; }
        public string LastEdited { get; set; }

        // Relations
        [Required]
        public int? SenderId { get; set; }
        [Required]
        public int? ReceiverId { get; set; }
        public int? QuestionId { get; set; }
    }
}