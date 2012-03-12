using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class UserMeta
    {
        // guid
        public int UserMetaID { get; set; }

        // Data
        public int Questions { get; set; }
        public int BestAnswers { get; set; }
        public int Votes { get; set; }
        public int Answers { get; set; }
        public int Tags { get; set; }

        // Relations
        [Required]
        public int? UserId { get; set; }
    }
}