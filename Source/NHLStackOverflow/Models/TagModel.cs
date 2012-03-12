using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models
{
    public class Tag
    {
        // GUID
        public int TagID { get; set; }

        // Data
        [Required]
        // regular expression missing
        public string Name { get; set; }

        [Required]
        // still needs being sanitized
        [MinLength(10, ErrorMessage="The minimum lenght is 10 characters")]
        [MaxLength(500, ErrorMessage="The maximum lenght is 500 characters")]
        public string Description { get; set; }

        [Required]
        public int Count { get; set; }
    }
}