using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Models.Badges
{
    public static class AnswerLordBadge
    {
        private static NHLdb db = new NHLdb();
        const int badgeID = 7;
        const string badgeName = "De Allesweter";
        const string badgeDescription = "Geef je 100e antwoord";
        const int RequiredAmmount = 100;
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

            if (userInfo.Answers == RequiredAmmount && alreadyBadge.Count() == 0)
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
            Message newMessage = new Message() { Content = "Hallo, \n U hebt een badge verdiend door uw 100e antwoord te geven. \n De Badgenaam is: De Allesweter", ReceiverId = userID, SenderId = userID, Title = "Je hebt de De Allesweter Badge verdient" };
            db.Messages.Add(newMessage);
            db.SaveChanges();
        }
    }
}