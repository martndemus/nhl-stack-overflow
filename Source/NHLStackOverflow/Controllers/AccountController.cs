using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;
using NHLStackOverflow.FormDataModels;

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
        public ActionResult LogIn(Login user, string returnUrl)
        {
            // Check if both Username and Password are present
            if (user.Password != null && user.UserName != null)
            {
                // Find a user with the given username in the database
                var userPass = from memberUser in db.Users
                               where memberUser.UserName == user.UserName
                               select memberUser.Password;

                // If the user exists check if the password is correct
                if (userPass.Count() == 1 && PasswordHasher.Hash(user.Password) == userPass.First())
                {
                    // User is logged in, set a cookie
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    
                    // Go to home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Display an error
                    ModelState.AddModelError("", "Ongeldig gebruikersnaam of wachtwoord.");
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
