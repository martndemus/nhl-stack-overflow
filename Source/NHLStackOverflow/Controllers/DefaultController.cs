using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Classes;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{ 
    public class DefaultController : Controller
    {
        private NHLdb db = new NHLdb();

        //
        // GET: /

        public ViewResult Index()
        {
            var QuestionList = from questions in db.Questions
                               orderby questions.Created_At descending
                               select questions;

            foreach (Question vraag in QuestionList)
            {
                vraag.Created_At = StringToDateTime.toSmootherTime(StringToDateTime.toDateTime(vraag.Created_At));
            }
 
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