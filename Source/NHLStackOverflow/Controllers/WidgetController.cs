using System;
using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Controllers
{
    public class WidgetController : Controller
    {
        private NHLdb db = new NHLdb();

        public ViewResult User()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // Find the user
                var User = (from u in db.Users
                            where u.UserName == HttpContext.User.Identity.Name
                            select u).Single();

                // Get the usermetadata
                var Usermeta = (from um in db.UserMeta
                                where um.UserId == User.UserID
                                select um).Single();

                // Get the amount of badges the user has
                var BadgeCount = (from b in db.Badges
                              where b.UserId == User.UserID
                              select b).Count();

                // get the amount of unread messages
                var MessageCount = (from m in db.Messages
                                    where m.Viewed == 0 && m.ReceiverId == User.UserID
                                    select m).Count();

                // Add user info to viewbag
                ViewBag.User = User;
                ViewBag.Usermeta = Usermeta;
                ViewBag.BadgeCount = BadgeCount;
                ViewBag.UnreadMessages = MessageCount;

                // Gravater url for user Avater
                ViewBag.GravatarURL = String.Format("http://www.gravatar.com/avatar/{0}?s=86&d=mm&r=g", Cryptography.GravatarHash(User.Email));
            }

            return View();
        }

        public ViewResult Account()
        {

            return View();
        }

        public ViewResult Tags()
        {
            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;
            ViewBag.TagList = TagsList;

            return View();
        }
    }
}
