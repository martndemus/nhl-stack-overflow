using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;

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
            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;
            ViewBag.TagList = TagsList;

            return View();
        }

        //
        // GET: /Tags/search/int TagID
        public ActionResult Search(int id)
        {
            var TagsSearched = from questionView in db.Questions
                               join c in db.QuestionTags on questionView.QuestionID equals c.QuestionId
                               where c.TagId == id
                               select questionView;
            ViewBag.TagSearch = TagsSearched;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
