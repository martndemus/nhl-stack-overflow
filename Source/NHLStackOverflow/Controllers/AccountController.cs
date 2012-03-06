using System.Web.Mvc;
using System.Web.Security;
using NHLStackOverflow.Models;
using System.Linq;
using System;

namespace NHLStackOverflow.Controllers
{
    public class AccountController : Controller
    {
        private NHLdb db = new NHLdb();
        //
        // GET: /Account/LogIn

        public ActionResult LogIn()
        {
            return View();
        }

        //
        // POST: /Account/LogIn

        [HttpPost]
        public ActionResult LogIn(LogOnModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // fields are filled in correctly
                var userPass = from memberUser in db.Users
                               where memberUser.UserName == user.UserName
                               select memberUser.Password;
                if (userPass.Count() == 1 && user.Password == userPass.First())
                {
                    // submittings were correct.
                    FormsAuthentication.SetAuthCookie(user.UserName, user.stayLoggenIn);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The username or password didn't match. Please try again.");
                }
            }

            return View(user);
        }

        //
        // GET: /Account/LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
