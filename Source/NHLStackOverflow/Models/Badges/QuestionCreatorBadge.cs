using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Models.Badges
{
    public static class QuestionCreatorBadge
    {
        private static NHLdb db = new NHLdb();
        const int badgeID = 8;
        public const string badgeName = "De Vrager";
        public const string badgeDescription = "Stel je 20e vraag";
        const int RequiredAmmount = 20;
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
            Message newMessage = new Message() { Content = "Hallo, \n U hebt een badge verdiend door uw 20e vraag te stellen. \n De Badgenaam is: De Vrager", ReceiverId = userID, SenderId = userID, Title = "Je hebt de De Vrager Badge verdient" };
            db.Messages.Add(newMessage);
            db.SaveChanges();
        }
    }
}