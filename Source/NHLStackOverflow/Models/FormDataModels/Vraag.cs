using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NHLStackOverflow.Models.FormDataModels
{
    public class Vraag
    {
        public string tags { get; set; }
        [MinLength(10)]
        [MaxLength(140)]
        public string vraag { get; set; }
        [MinLength(140)]
        public string content { get; set; }

        [MinLength(10, ErrorMessage = "De minimale lengte is 10 karakters.")]
        [MaxLength(500, ErrorMessage = "De maximale lengte is 500 karakters.")]
        public string tag0 { get; set; }

        [MinLength(10, ErrorMessage = "De minimale lengte is 10 karakters.")]
        [MaxLength(500, ErrorMessage = "De maximale lengte is 500 karakters.")]
        public string tag1 { get; set; }

        [MinLength(10, ErrorMessage = "De minimale lengte is 10 karakters.")]
        [MaxLength(500, ErrorMessage = "De maximale lengte is 500 karakters.")]
        public string tag2 { get; set; }

        [MinLength(10, ErrorMessage = "De minimale lengte is 10 karakters.")]
        [MaxLength(500, ErrorMessage = "De maximale lengte is 500 karakters.")]
        public string tag3 { get; set; }

        [MinLength(10, ErrorMessage = "De minimale lengte is 10 karakters.")]
        [MaxLength(500, ErrorMessage = "De maximale lengte is 500 karakters.")]
        public string tag4 { get; set; }

        [MinLength(10, ErrorMessage = "De minimale lengte is 10 karakters.")]
        [MaxLength(500, ErrorMessage = "De maximale lengte is 500 karakters.")]
        public string tag5 { get; set; }
    }

    public class CommentAnswer
    {
        [MinLength(140, ErrorMessage = "De inhoud van een antwoord moet minstens 140 karakters lang zijn.")]
        public string awnser { get; set; }

        [MinLength(10, ErrorMessage = "De inhoud van een comment moet minstens 10 karakters lang zijn.")]
        public string awnserComment { get; set; }
        [MinLength(10, ErrorMessage = "De inhoud van een comment moet minstens 10 karakters lang zijn.")]
        public string questionComment { get; set; }

        public int awnserID { get; set; }
    }

    public static class tagsCount
    {
        /// <summary>
        /// Count the amount of tag descriptions sumbmitted
        /// </summary>
        /// <returns>The amount of tags submitted</returns>
        public static int CountTagsSumbitted(this Vraag vraagje )
        {
            int counter = 0;
            if (vraagje.tag0 != null)
            {
                counter++;
            }
            if (vraagje.tag1 != null)
            {
                counter++;
            }
            if (vraagje.tag2 != null)
            {
                counter++;
            }
            if (vraagje.tag3 != null)
            {
                counter++;
            }
            if (vraagje.tag4 != null)
            {
                counter++;
            }
            if (vraagje.tag5 != null)
            {
                counter++;
            }
            return counter;
        }

        /// <summary>
        /// Return the given tag description at the point of element
        /// </summary>
        /// <param name="vraagje">Extension on the </param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string returnTagContent(this Vraag vraagje, int element)
        {
            switch (element)
            {
                case 0:
                    return vraagje.tag0;
                case 1:
                    return vraagje.tag1;
                case 2:
                    return vraagje.tag2;
                case 3:
                    return vraagje.tag3;
                case 4:
                    return vraagje.tag4;
                case 5:
                    return vraagje.tag5;
            }
            return null; // if we get here we are out of bounds so return null
        }
    }

    
}