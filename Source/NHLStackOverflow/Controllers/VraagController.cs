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
            ViewBag.QuestionDetail = questionDetails.First();

            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;
            ViewBag.TagList = TagsList;

            var useringlist = from users in db.Users
                           where users.UserID == id
                           select users;
            ViewBag.QuestionUser = useringlist.First();

            return View();
        }

    }
}
