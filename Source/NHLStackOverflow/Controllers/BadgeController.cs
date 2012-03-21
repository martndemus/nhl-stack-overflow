using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{
    public class BadgeController : Controller
    {

        private NHLdb db = new NHLdb();
        //
        // GET: /Badge/

        public ActionResult Index()
        {

            var badgeList = from badges in db.Badges
                            orderby badges.Name ascending
                            select badges;
            ViewBag.badgesList = badgeList;

            return View();
        }

    }
}
