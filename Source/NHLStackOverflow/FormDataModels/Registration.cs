using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHLStackOverflow.FormDataModels
{
    public class Registration
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string PassWord2 { get; set; } // the second box which should match the first, to prevent mistaken passwords getting filled in
        public bool ConditionsAccepted { get; set; }
        public string email { get; set; }

    }
}