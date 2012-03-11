using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class QuestionTag
    {
        // GUID
        public int QuestionTagID { get; set; }

        // Relations
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int TagId { get; set; }
    }
}