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

        public ActionResult Index()
        {
            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;
            ViewBag.TagList = TagsList;

            return View();
        }

        //
        // GET: /Question/detailNum
        public ActionResult Details()//int detailID)
        {
            int detailID = 2;
            var questionDetails = from questionDetail in db.Questions
                                  where questionDetail.QuestionID == detailID
                                  select questionDetail;
            ViewBag.QuestionDetail = questionDetails;
            return View();
        }

    }
}
