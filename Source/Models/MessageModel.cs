using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    /// <summary>
    /// Summary description for Message
    /// </summary>
    public class Message
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public User Sender { get; set; }

        [Required]
        public User Receiver { get; set; }

        [Required]
        public Question Post { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime  Date { get; set; }

        [Required]
        public string Text { get; set; }
    }
}