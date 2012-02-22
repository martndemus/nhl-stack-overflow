using System;
using System.Collections.Generic;
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
        [MinLength(2, ErrorMessage="The minimum lenght is 2 characters")]
        [MaxLength(500, ErrorMessage="The maximum lenght is 500 characters")]
        public string Beschrijving { get; set; }

        // Relations
        public ICollection<QuestionTag> Tags { get; set; }
    }
}