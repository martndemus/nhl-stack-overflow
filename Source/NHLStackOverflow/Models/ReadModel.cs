using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Read
    {
        // GUID
        public int ReadID { get; set; }

        // Relations
        [Required]
        public int? UserId { get; set; }
        [Required]
        public int? QuestionId { get; set; }
    }
}