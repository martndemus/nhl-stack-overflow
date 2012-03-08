using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{ 
    public class HomeController : Controller
    {
        private NHLdb db = new NHLdb();

        //
        // GET: /

        public ViewResult Index()
        {
            db.Database.Initialize(true);
            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;
            ViewBag.TagList = TagsList;

            var QuestionList = from questions in db.Questions
                               orderby questions.Created_At descending
                               select questions;
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