using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{
    public class VraagController : Controller
    {
        private NHLdb db = new NHLdb();
        //
        // GET: /Question/

        //public ActionResult Index()
        //{
        //    var TagsList = from tags in db.Tags
        //                   orderby tags.Count descending
        //                   select tags;
        //    ViewBag.TagList = TagsList;

        //    return View();
        //}

        //
        // GET: /Question/detailNum
        public ActionResult View(int id)
        {
            var questionDetails = from questionDetail in db.Questions
                                  where questionDetail.QuestionID == id
                                  select questionDetail;

            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;
            ViewBag.TagList = TagsList;

            ViewBag.QuestionDetail = questionDetails.First();
            return View();
        }

    }
}
