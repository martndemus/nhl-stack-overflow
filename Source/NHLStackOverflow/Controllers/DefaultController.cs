using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Classes;
using NHLStackOverflow.Models;
using System;
using System.Collections.Generic;

namespace NHLStackOverflow.Controllers
{ 
    public class DefaultController : Controller
    {
        private NHLdb db = new NHLdb();

        //
        // GET: /

        public ViewResult Index()
        {
            HTMLSanitizer hs = new HTMLSanitizer();

            var QuestionList = from questions in db.Questions
                               orderby questions.Created_At descending
                               select questions;
            List<User> usersList = new List<User>();
            List<TagsIDs> abc = new List<TagsIDs>();
            foreach (Question vraag in QuestionList)
            {
                // Strip all HTML tags from the question content
                vraag.Content = hs.Sanitize(vraag.Content, HTMLSanitizerOption.StripTags);

                // Limit the content of a question on the front page to max 500 chars.
                if (vraag.Content.Length > 500)
                    vraag.Content = vraag.Content.Substring(0, 500) + " ...";

                // Build a list of tags that this question is tagged with.
                var TagList = from tagsQuestion in db.Tags
                              join c in db.QuestionTags on tagsQuestion.TagID equals c.TagId
                              where c.QuestionId == vraag.QuestionID
                              select tagsQuestion;

                foreach (Tag i in TagList)
                    abc.Add(new TagsIDs(i, vraag.QuestionID));
                
                var UserList = from users in db.Users
                               where users.UserID == vraag.UserId
                               select users;
                if(!usersList.Contains(UserList.First()))
                    usersList.Add(UserList.First());
            }

            ViewBag.Helper = abc;
            ViewBag.UsersList = usersList;
            ViewBag.QuestionList = QuestionList;

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}