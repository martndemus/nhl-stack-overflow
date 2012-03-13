using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;
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
            var QuestionList = from questions in db.Questions
                               orderby questions.Created_At descending
                               select questions;
            List<TagsIDs> abc = new List<TagsIDs>();
            foreach (Question vraag in QuestionList)
            {
                var TagList = from tagsQuestion in db.Tags
                              join c in db.QuestionTags on tagsQuestion.TagID equals c.TagId
                              where c.QuestionId == vraag.QuestionID
                              select tagsQuestion;
                foreach (Tag i in TagList)
                    abc.Add(new TagsIDs(i, vraag.QuestionID));
            }
            ViewBag.TagsList = abc;
            ViewBag.QuestionList = QuestionList;
            // from tagsQuestion in db.Tags
            // onner
            //join c in db.QuestionTags on questionView.QuestionID equals c.QuestionId
            //where c.TagId == id

            //ViewBag.Tagslist = Taglist;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}