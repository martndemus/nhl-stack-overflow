using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using NHLStackOverflow.Classes;
using NHLStackOverflow.FormDataModels;
using NHLStackOverflow.Models;
using NHLStackOverflow.Mailers;
using Mvc.Mailer;

namespace NHLStackOverflow.Controllers
{
    public class LoginController : Controller
    {
        // the stuff needed to be able to send a real e-mail
        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }
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
        public ActionResult Index(Login user)
        {
            // Check if both Username and Password are present
            if (user.Password != null && user.UserName != null)
            {
                // Find a user with the given username in the database
                var userPass = from memberUser in db.Users
                               where memberUser.UserName == user.UserName
                               select memberUser;

                // If the user exists check if the password is correct
                if (userPass.Count() == 1 && Cryptography.PasswordHash(user.Password) == userPass.First().Password)
                {
                    if (userPass.First().Activated == 1)
                    {
                        // User is logged in, set a cookie
                        FormsAuthentication.SetAuthCookie(user.UserName, true);

                        // Go to home page
                        return RedirectToAction("index", "default");
                    }
                    else
                        ModelState.AddModelError("", "Dit account is nog niet geactiveerd. Controleer uw inbox aub.");
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
        public ActionResult WachtwoordKwijt(PassLostEmail user)
        {
            // get this info of the user that has lost his password
            var passLostPerson = from userPassLost in db.Users
                                 where userPassLost.Email == user.Email && userPassLost.Activated == 1 
                                 && userPassLost.PassLost == null
                                 select userPassLost;
            // if the didn't yield a result show an error
            if (passLostPerson.Count() != 1)
                ModelState.AddModelError("", "Er is geen juiste user gevonden.");

            // if this is valid
            if(ModelState.IsValid)
            {
                // in a try catch so that if mailing failed we can try again
                try
                {
                    passLostPerson.First().PassLost = Cryptography.UrlHash(passLostPerson.First().Password + passLostPerson.First().Email);
                    // generate a mail and send it.
                    UserMailer.MailPassForgotten(passLostPerson.First().PassLost, passLostPerson.First().Email).Send();
                }
                catch
                {
                    ModelState.AddModelError("", "Er is iets mis gegaan. Onze excuses voor het ongemak. Probeer het over enkele momenten overnieuw.");
                }
                // if it all went right link us to the succes page and save the datebase changes
                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                    return RedirectToAction("kwijtverstuurd");
                }
            }


            return View();
        }

        //
        // GET: /login/kwijtVeranderen
        public ActionResult KwijtVeranderen(string id)
        {
            return View();
        }

        // 
        // POST: /login/kwijtveranderen
        [HttpPost]
        public ActionResult KwijtVeranderen(string id, PassLost user)
        {
            // check if the two passwords are equal
            if (user.Password1 != user.Password2)
                ModelState.AddModelError("", "Het eerste en het tweede wachtwoord kwamen niet overeen.");
            // get the info of the person that has lost his password
            var kwijtUser = from userKwijt in db.Users
                            where userKwijt.PassLost == id
                            select userKwijt;
            // check if this yielded some result
            if (kwijtUser.Count() != 1)
                ModelState.AddModelError("", "De meegegeven string komt niet overeen met een gebruiker.");
            // if so continue
            if (ModelState.IsValid)
            {
                // set the new password
                kwijtUser.First().Password = Cryptography.PasswordHash(user.Password1);
                kwijtUser.First().PassLost = null;
                db.SaveChanges();

                // to a static page saying it has been changed
                return RedirectToAction("wachtwoordveranderd", "Login");
            }
            // if we get here it wasn't valid
            return View();
        }

        // 
        // GET: /login/wachtwoordveranderd
        public ActionResult WachtwoordVeranderd()
        {
            return View();
        }

        //
        // GET: /login/kwijtverstuurd
        public ActionResult KwijtVerstuurd()
        {
            return View();
        }

        //
        // GET: /login/logout
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "default");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}