using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Classes
{
    /// <summary>
    /// A litle class which holds tag and the questionID that has this tag
    /// </summary>
    public class TagsIDs
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tag">The tag that should be linked with</param>
        /// <param name="questionTaggyID">this questionID</param>
        public TagsIDs(Tag tag, int questionTaggyID)
        {
            this.Tag = tag;
            this.QuestionTaggyID = questionTaggyID;
        }
        public Tag Tag { get; set; }
        public int QuestionTaggyID { get; set; }
    }
}