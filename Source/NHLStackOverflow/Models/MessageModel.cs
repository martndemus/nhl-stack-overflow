using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace NHLStackOverflow.Models
{
    public class Message
    {
        // GUID
        public int MessageID { get; set; }

        // Data
        public string Title { get; set; }
        public string Content { get; set; }

        // Timestamps
        public DateTime Date { get; set; }
        public DateTime LastEdited { get; set; }

        // Relations
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public Question Post { get; set; }
    }
}