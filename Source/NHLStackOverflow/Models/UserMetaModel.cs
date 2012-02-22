using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class UserMeta
    {
        public UserMeta()
        {
            AantalAnswers = 0;
            AantalBestAnswers = 0;
            AantalQuestions = 0;
            TotalVotes = 0;
        }

        public int UserID { get; set; }

        public int AantalQuestions { get; set; }
        public int AantalBestAnswers { get; set; }
        public int TotalVotes { get; set; }
        public int AantalAnswers { get; set; }


        // Relations
        public User User { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}