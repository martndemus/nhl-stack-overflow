using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Controllers
{
    public class ProfielController : Controller
    {
        private NHLdb db = new NHLdb();
        //
        // GET: /Profiel/

        [HttpGet()]
        public ActionResult Index(string username)
        {
            // Find the user
            var User = (from u in db.Users
                            where u.UserName == username
                            select u).Single();

            // Get the usermetadata
            var Usermeta = (from um in db.UserMeta
                            where um.UserId == User.UserID
                            select um).Single();

           

            // Add user info to viewbag
            ViewBag.User        = User;
            ViewBag.Usermeta    = Usermeta;

            // Gravater url for user Avater
            ViewBag.GravatarURL = String.Format("http://www.gravatar.com/avatar/{0}?s=92&d=mm&r=g", Cryptography.GravatarHash(User.Email));

            return View();
        }

    }
}
