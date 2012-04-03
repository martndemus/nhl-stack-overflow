using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Classes;
using NHLStackOverflow.Models;
using System.Collections.Generic;

namespace NHLStackOverflow.Controllers
{
    public class TagsController : Controller
    {
        private NHLdb db = new NHLdb(); 
        //
        // GET: /Tags/
        // Tag overview pls :D
        public ActionResult Index()
        {
            HTMLSanitizer hs = new HTMLSanitizer();

            // get a list of all the tags
            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;

            
            foreach (var tag in TagsList)
            {
                // Escape the text in each tag
                tag.Name = hs.EscapeHTMLentities(tag.Name);
                tag.Description = hs.EscapeHTMLentities(tag.Description);
            }

            ViewBag.TagList = TagsList;

            return View();
        }

        //
        // GET: /Tags/search/int TagID
        public ActionResult Search(int id)
        {
            HTMLSanitizer hs = new HTMLSanitizer();
            // get all the questions which have this tag
            var TagsSearched = from questionView in db.Questions
                               join c in db.QuestionTags on questionView.QuestionID equals c.QuestionId
                               where c.TagId == id
                               select questionView;

            ViewBag.TagSearch = TagsSearched;

            // get the info about this tag
            var TagsNames = from tagsName in db.Tags
                            where tagsName.TagID == id
                            select tagsName;
            ViewBag.TagNames = TagsNames.First();

            List<TagsIDs> abc = new List<TagsIDs>();
            List<User> userTags = new List<User>();

            // Get all the tags of each question and the user(for the username)
            foreach (Question t in TagsSearched)
            {
                // get all the tags
                var TagList = from tagsQuestion in db.Tags
                              join c in db.QuestionTags on tagsQuestion.TagID equals c.TagId
                              where c.QuestionId == t.QuestionID
                              select tagsQuestion;
                // add each to the list
                foreach (Tag i in TagList)
                {
                    abc.Add(new TagsIDs(i, t.QuestionID));

                    // Sanitize each tag its name
                    i.Name = hs.Sanitize(i.Name, HTMLSanitizerOption.StripTags);
                }
                // get the user stuff
                var UsersList = from users in db.Users
                               where users.UserID == t.UserId
                               select users;
                // if not in the list add it
                if (!userTags.Contains(UsersList.First()))
                    userTags.Add(UsersList.First());

                
            }
            ViewBag.usersTags = userTags;
            ViewBag.Helper = abc;

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
