using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models.FormDataModels
{
    public class Mail
    {

        [Required(ErrorMessage = "De titel is verplicht en dient tussen de 10 en 140 karakters lang te zijn.")]
        [MinLength(10)]
        [MaxLength(140)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Het bericht dient te zijn ingevuld. (minimaal 10 karakters lang)")]
        [MinLength(10)]
        public string Content { get; set; }
        [Required(ErrorMessage = "De persoon om naartoe te sturen dient in gevuld te zijn.")]
        public string SendTo { get; set; }

    }
}