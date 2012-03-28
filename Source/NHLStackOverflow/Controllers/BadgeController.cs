using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Controllers
{
    public class BadgeController : Controller
    {

        private NHLdb db = new NHLdb();
        //
        // GET: /Badge/

        public ActionResult Index()
        {

            var badgeList = from badges in db.Badges
                            group badges by badges.Name into a
                            select new BadgeCount() { badge = a.FirstOrDefault(), count = a.Count() };
            ViewBag.badgesList = badgeList;

            return View();
        }

        public ActionResult Search(string id)
        {
            var badgeList = from badges in db.Badges
                            where badges.Name == id
                            select badges;
 
            ViewBag.badgesList = badgeList;
            ViewBag.badgesName = badgeList.First();
            ViewBag.badgeCount = badgeList.Count();
            List<User> test = new List<Models.User>();
            foreach (var badgde in badgeList)
            {
                var badgeUsers = from users in db.Users
                                 orderby users.Created_At descending
                                 where users.UserID == badgde.UserId
                                 select users;
                test.Add(badgeUsers.First());
            }
            ViewBag.badgesUsers = test;
            return View();
        }

    }
}
