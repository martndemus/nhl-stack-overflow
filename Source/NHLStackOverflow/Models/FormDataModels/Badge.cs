using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHLStackOverflow.Models.FormDataModels
{
    public class BadgeDe
    {
        public BadgeDe(int badgeID, string description, string color)
        {
            this.BadgeID = badgeID;
            this.Description = description;
            this.Color = color;
        }
        public int BadgeID { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}