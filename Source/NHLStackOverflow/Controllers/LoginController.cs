using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NHLStackOverflow.Classes;
using NHLStackOverflow.FormDataModels;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{
    public class LoginController : Controller
    {
        private NHLdb db = new NHLdb();

        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Login/

        [HttpPost]
        public ActionResult Index(Login user, string returnUrl)
        {
            // Check if both Username and Password are present
            if (user.Password != null && user.UserName != null)
            {
                // Find a user with the given username in the database
                var userPass = from memberUser in db.Users
                               where memberUser.UserName == user.UserName
                               select memberUser;

                // If the user exists check if the password is correct
                if (userPass.Count() == 1 && PasswordHasher.Hash(user.Password) == userPass.First().Password)
                {
                    if (userPass.First().Activated == 1)
                    {
                        // User is logged in, set a cookie
                        FormsAuthentication.SetAuthCookie(user.UserName, true);

                        // Go to home page
                        return RedirectToAction("index", "home");
                    }
                    else
                        ModelState.AddModelError("", "This account hasn't been activated yet, please check your e-mail box.");
                }
                else
                {
                    // Display an error
                    ModelState.AddModelError("", "Ongeldig gebruikersnaam of wachtwoord.");
                }
            }

            ViewBag.UserName = user.UserName;

            return View();
        }

        //
        // GET: /login/wachtwoordkwijt

        public ActionResult WachtwoordKwijt()
        {
            return View();
        }

        //
        // POST: /login/wachtwoordkwijt

        [HttpPost]
        public ActionResult WachtwoordKwijt(User user)
        {
            // Todo wachtwoordkwijt mailer maken

            return View();
        }

        //
        // GET: /login/logout

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}