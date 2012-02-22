using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class UserMeta
    {
        public UserMeta()
        {
            AantalAnwsers = 0;
            AantalBestAnwsers = 0;
            AantalQuestions = 0;
            TotalVotes = 0;
        }
        [Required]
        public int AantalQuestions { get; set; }
        [Required]
        public int AantalBestAnwsers { get; set; }
        [Required]
        public int TotalVotes { get; set; }
        [Required]        
        public int AantalAnwsers { get; set; }


        // Relations
        public User User { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}