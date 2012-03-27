using System;
using System.Linq;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Models.Badges
{
    public static class AnswerBadge
    {
        private static NHLdb db = new NHLdb();
        const int badgeID = 2;
        const string badgeName = "1e Antwoord";
        const string badgeDescription = "Geef je eerste antwoord";
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
            Message newMessage = new Message() { Content = "Hallo, \n U hebt een badge verdiend door uw eerste antwoord te geven. \n De Badgenaam is: 1e Antwoord, hoe mooi is dat wel niet", ReceiverId = userID, SenderId = userID, Title = "Je hebt de antwoordbadge verdient" };
            db.Messages.Add(newMessage);
            db.SaveChanges();

        }
    }
}