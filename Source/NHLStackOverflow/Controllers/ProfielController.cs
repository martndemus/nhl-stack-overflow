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
            // Find the user
            var User = (from u in db.Users
                        where u.UserName == username
                        select u).Single();

            // Get the usermetadata
            var Usermeta = (from um in db.UserMeta
                            where um.UserId == User.UserID
                            select um).Single();

            // Get all questions asked by the user
            List<Question> QuestionList = new List<Question>();
            var Questions = from q in db.Questions
                            orderby q.Created_At descending
                            where q.UserId == User.UserID
                            select q;

            QuestionList.AddRange(Questions);

            // Get all answers by the user
            var Answers = from a in db.Answers
                          orderby a.Created_At descending
                          where a.UserId == User.UserID
                          select a;

            // Create a list of Questions the user has answered on
            List<Question> QuestionsAnswered = new List<Question>();
            foreach (Answer a in Answers.Take(10))
            {
                var QuestionAnswered = (from qa in db.Questions
                                       where qa.QuestionID == a.QuestionId
                                       select qa).First();

                QuestionsAnswered.Add(QuestionAnswered);
            }                                    

            // Get all badges belonging to user
            var Badges = from b in db.Badges
                         orderby b.Created_At descending
                         where b.UserId == User.UserID
                         select b;

            // Generate a list of tags that belong to the user
            List<QuestionTag> Tags = new List<QuestionTag>();

            // First add all tags from questions posed by the user
            foreach (Question q in Questions)
            {
                var tags = from t in db.QuestionTags
                           where t.QuestionId == q.QuestionID
                           select t;

                foreach (QuestionTag t in tags)
                    Tags.Add(t);
            }

            // Then add all tags from questions the user answered on.
            foreach (Answer a in Answers)
            {
                var tags = from t in db.QuestionTags
                           where t.QuestionId == a.QuestionId
                           select t;

                foreach (QuestionTag t in tags)
                    Tags.Add(t);
            }

            // Select the distinct tags and count the amount of occurences
            var QTags = from t in Tags
                           group t by t.TagId into g
                           orderby g.Count() descending
                           select new { Tag = g.First(), Count = g.Count() };
           
                     
            // Now get the real tags from the db
            Dictionary<Tag, int> UserTags = new Dictionary<Tag,int>();
            foreach (var qt in QTags)
            {
                var UserTag = (from t in db.Tags
                              where t.TagID == qt.Tag.TagId 
                              select t).Single();

                UserTags.Add(UserTag, qt.Count); 
            }

            
            // Add user info to viewbag
            ViewBag.User              = User;
            ViewBag.Usermeta          = Usermeta;
            ViewBag.Questions         = QuestionList;
            ViewBag.QuestionsAnswered = QuestionsAnswered;
            ViewBag.Tags              = UserTags;
            ViewBag.Badges            = Badges;
            ViewBag.BadgeCount        = Badges.Count();

            // Gravater url for user Avater
            ViewBag.GravatarURL = String.Format("http://www.gravatar.com/avatar/{0}?s=92&d=retro&r=g", Cryptography.GravatarHash(User.Email));

            return View();
        }

        //
        // GET: Profiel/Overzicht

        public ActionResult Overzicht()
        {


            ViewBag.Users = db.Users;

            return View();
        }

    }
}
