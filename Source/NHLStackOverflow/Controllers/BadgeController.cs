using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;
using NHLStackOverflow.Models.FormDataModels;

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

            var badgeDesList = new List<BadgeDe>{
                new BadgeDe(1, "Stel je eerste vraag", "Bronze"),
                new BadgeDe(2, "Geef je eerste antwoord", "Bronze"),
                new BadgeDe(3, "Maak je eerste Tag aan", "Bronze"),
                new BadgeDe(4, "Maak je 20e Tag aan", "Zilver"),
                new BadgeDe(5, "Maak je 9000e Tag aan", "Platina"),
                new BadgeDe(6, "Geef je 20e antwoord", "Zilver"),
                new BadgeDe(7, "Geef je 100e antwoord", "Goud"),
                new BadgeDe(8, "Stel je 20e vraag", "Zilver"),
                new BadgeDe(9, "Stel je 100e vraag", "Goud")
            };

            ViewBag.BadgeDescription = badgeDesList;

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

            var badgeDesList = new List<BadgeDe>{
                new BadgeDe(1, "Stel je eerste vraag", "Bronze"),
                new BadgeDe(2, "Geef je eerste antwoord", "Bronze"),
                new BadgeDe(3, "Maak je eerste Tag aan", "Bronze"),
                new BadgeDe(4, "Maak je 20e Tag aan", "Zilver"),
                new BadgeDe(5, "Maak je 9000e Tag aan", "Platina"),
                new BadgeDe(6, "Geef je 20e antwoord", "Zilver"),
                new BadgeDe(7, "Geef je 100e antwoord", "Goud"),
                new BadgeDe(8, "Stel je 20e vraag", "Zilver"),
                new BadgeDe(9, "Stel je 100e vraag", "Goud")
            };
            int idtje = badgeList.First().BadgeID;
            ViewBag.BadgeDescription = (from badgeDescript in badgeDesList
                                        where badgeDescript.BadgeID == idtje
                                        select badgeDescript.Description).First();

            return View();
        }

    }
}
