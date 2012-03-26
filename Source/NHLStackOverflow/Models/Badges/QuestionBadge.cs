using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHLStackOverflow.Classes;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Models.Badges
{
    public static class BronzeQuestionBadge// : AbstractBadge
    {
        private static NHLdb db = new NHLdb();
        const int badgeID = 1;
        const string badgeName = "QuestionAskerMaster";
        const int RequiredAmmount = 1;
        public static bool badgeAchieve(int userID)
        {
            UserMeta userInfo = (from user in db.UserMeta
                           where user.UserId == userID
                           select user).Single();

            var alreadyBadge = from badges in db.Badges
                               where badges.UserId == userID && badges.Name == badgeName
                               select badges;
            //if (userInfo.Count() == 1)
            //    continue;
         
            if (userInfo.Questions == RequiredAmmount && alreadyBadge.Count() == 0)
                return true;
            else
                return false;
        }

        public static void awardBadge(int userID)
        {
            var userInfo = (from user in db.UserMeta
                                 where user.UserId == userID
                                 select user).Single();

                Badge newBadge = new Badge() { Name = badgeName, UserId = userID, Created_At = StringToDateTime.ToUnixTimeStamp(DateTime.Now) };
                db.Badges.Add(newBadge);
                db.SaveChanges();
           
        }
    }
}