using System;
using System.ComponentModel.DataAnnotations;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Models
{
    public class Badge
    {
        public Badge()
        {
            this.Created_At = StringToDateTime.ToUnixTimeStamp(DateTime.Now);
        }

        // GUID
        [Required]
        public int BadgeID { get; set; }

        // Data
        [Required]
        public string Name { get; set; }

        // TimeStamps
        [Required]
        public double Created_At { get; set; }

        // Relations
        [Required]
        public int UserId { get; set; }
    }
}