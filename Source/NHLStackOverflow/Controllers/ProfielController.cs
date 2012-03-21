using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Controllers
{
    public class ProfielController : Controller
    {
        private NHLdb db = new NHLdb();
        //
        // GET: /Profiel/

        [HttpGet()]
        public ActionResult Index(string username)
        {
            List<QuestionTag> Tags = new List<QuestionTag>();

            // Find the user
            var User = (from u in db.Users
                            where u.UserName == username
                            select u).Single();

            // Get the usermetadata
            var Usermeta = (from um in db.UserMeta
                            where um.UserId == User.UserID
                            select um).Single();

            // Get all questions asked by the user
            var Questions = from q in db.Questions
                            where q.UserId == User.UserID
                            select q;

            // Get all answers by the user
            var Answers = from a in db.Answers
                          where a.UserId == User.UserID
                          select a;

            // Get all tags by the user            
            foreach (Question q in Questions)
            {
                var tags = from t in db.QuestionTags
                           where t.QuestionId == q.QuestionID
                           select t;

                foreach (QuestionTag t in tags)
                    Tags.Add(t);
            }

            // Get all badges belonging to user
            var Badges = from b in db.Badges
                         where b.UserId == User.UserID
                         select b;

            // Add user info to viewbag
            ViewBag.User        = User;
            ViewBag.Usermeta    = Usermeta;
            ViewBag.Questions   = Questions;
            ViewBag.Answers     = Answers;
            ViewBag.Tags        = Tags.Distinct();
            ViewBag.Badges      = Badges;

            // Gravater url for user Avater
            ViewBag.GravatarURL = String.Format("http://www.gravatar.com/avatar/{0}?s=92&d=mm&r=g", Cryptography.GravatarHash(User.Email));

            return View();
        }

    }
}
