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
            AantalTags = 0;
        }

        public int UserMetaID { get; set; }

        [Required]
        public int AantalQuestions { get; set; }
        [Required]
        public int AantalBestAnswers { get; set; }
        [Required]
        public int TotalVotes { get; set; }
        [Required]
        public int AantalAnswers { get; set; }
        [Required]
        public int AantalTags { get; set; }

        // Relations
        [Required]
        public int UserId { get; set; }
    }
}