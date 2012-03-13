using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using NHLStackOverflow.Models;
using NHLStackOverflow.Models.FormDataModels;

namespace NHLStackOverflow.Controllers
{
    public class UserController : Controller
    {
        private NHLdb db = new NHLdb();
        //
        // GET: /User/

        public ActionResult Index()
        {
            //string test = User.Identity.Name;
            var userInfo = from user in db.Users
                           where user.UserName == User.Identity.Name
                           select user;
            if (userInfo.Count() == 1)
            {
                // Give back all the info off the person
                ViewBag.User = userInfo.First();
            }
            return View();
        }

        //
        // POST: /User/
        [HttpPost]
        public ActionResult Index(Account newUserInfo)
        {
            //string test = "Hallo wererld";
            var userInfo = from user in db.Users
                           where user.UserName == User.Identity.Name
                           select user;
            if(ModelState.IsValid)
            {
                userInfo.First().Location = newUserInfo.Location;
                userInfo.First().Name = newUserInfo.Name;
                userInfo.First().Birthday = newUserInfo.Birthday;
                userInfo.First().Languages = newUserInfo.Languages;
                db.SaveChanges();
            }

            ViewBag.User = userInfo.First();

            return View();
        }


    }
}
