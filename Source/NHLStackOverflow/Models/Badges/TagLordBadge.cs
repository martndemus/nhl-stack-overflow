using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Models.Badges
{
    public static class TagLordBadge
    {
        private static NHLdb db = new NHLdb();
        const int badgeID = 5;
        const string badgeName = "9000e Tag";
        const string badgeDescription = "Maak je 9000e Tag aan";
        const int RequiredAmmount = 9000;
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

            if (userInfo.Tags >= RequiredAmmount && alreadyBadge.Count() == 0)
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
            Message newMessage = new Message() { Content = "Hallo, \n U hebt een badge verdiend door uw 9000e Tag aan te maken. van harte gefeliciteerd! \n De Badgenaam is: 9000e Tag", ReceiverId = userID, SenderId = userID, Title = "Je hebt de 9000e tag Badge verdient" };
            db.Messages.Add(newMessage);
            db.SaveChanges();

        }
    }
}