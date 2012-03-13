using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Classes
{
    public class TagsIDs
    {
        public TagsIDs(Tag tag, int questionTaggyID)
        {
            this.Tag = tag;
            this.QuestionTaggyID = questionTaggyID;
        }
        public Tag Tag { get; set; }
        public int QuestionTaggyID { get; set; }
    }
}