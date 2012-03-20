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
            // Get the name of the tag.
            ViewBag.TagName = (from t in db.Tags
                               where t.TagID == id
                               select  t.Name).Single();

            HTMLSanitizer hs = new HTMLSanitizer();

            var TagsSearched = from questionView in db.Questions
                               join c in db.QuestionTags on questionView.QuestionID equals c.QuestionId
                               where c.TagId == id
                               select questionView;

            ViewBag.TagSearch = TagsSearched;

            var TagsNames = from tagsName in db.Tags
                            where tagsName.TagID == id
                            select tagsName;
            ViewBag.TagNames = TagsNames.First();

            List<TagsIDs> abc = new List<TagsIDs>();

            //tags in question

            foreach (Question t in TagsSearched)
            {
                var TagList = from tagsQuestion in db.Tags
                              join c in db.QuestionTags on tagsQuestion.TagID equals c.TagId
                              where c.QuestionId == t.QuestionID
                              select tagsQuestion;

                foreach (Tag i in TagList)
                {
                    abc.Add(new TagsIDs(i, t.QuestionID));

                    // Sanitize each tag its name
                    i.Name = hs.Sanitize(i.Name, HTMLSanitizerOption.StripTags);
                }
            }
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
