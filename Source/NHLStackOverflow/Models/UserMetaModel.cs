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
        public int AantalQuestions { get; set; }
        public int AantalBestAnwsers { get; set; }
        public int TotalVotes { get; set; }
        public int AantalAnwsers { get; set; }


        // Relations
        public User User { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}