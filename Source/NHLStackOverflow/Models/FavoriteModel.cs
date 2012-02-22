using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Favorite : IValidatableObject
    {
        public Favorite()
        {
            this.Created_At = DateTime.Now.ToString();
        }
        // GUID
        [Required]
        public int FavoriteID { get; set; }

        // TimeStamp
        [Required]
        public string Created_At { get; set; }

        // Relations
        [Required]
        public User User { get; set; }
        [Required]
        public Question Post { get; set; }
    }
}