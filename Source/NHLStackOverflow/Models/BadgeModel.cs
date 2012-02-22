using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Badge
    {
        public Badge()
        {
            this.Created_At = DateTime.Now.ToString();
        }
        // GUID
        [Required]
        public int BadgeID { get; set; }

        // Data
        [Required]
        public string Name { get; set; }

        // TimeStamps
        [Required]
        public string Created_At { get; set; }

        // Relations
        [Required]
        public User User { get; set; }
    }
}