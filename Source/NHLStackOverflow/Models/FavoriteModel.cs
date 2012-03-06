using System;
using System.ComponentModel.DataAnnotations;

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
        public int UserId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        

        //[Required]
        //public User User { get; set; }
        //[Required]
        //public Question Post { get; set; }
    }
}