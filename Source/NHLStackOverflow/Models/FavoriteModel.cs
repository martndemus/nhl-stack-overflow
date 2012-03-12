using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NHLStackOverflow.Models
{
    public class Favorite
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
        public int? UserId { get; set; }
        [Required]
        public int? QuestionId { get; set; }
    }
}