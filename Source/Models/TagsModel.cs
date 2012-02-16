using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Models
{
    public class Tags
    {
        [Required]
        [Key]
        public int TagID { get; set; }

        [Required]
        public string Beschrijving { get; set; }

    }
}