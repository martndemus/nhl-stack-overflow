using System.Linq;
using System.Web.Mvc;
using Mvc.Mailer;
using NHLStackOverflow.Classes;
using NHLStackOverflow.FormDataModels;
using NHLStackOverflow.Mailers;
using NHLStackOverflow.Models;
using System;

namespace NHLStackOverflow.Controllers
{
    public class RegistreerController : Controller
    {
        private IUserMailer _userMailer = new UserMailer();
        public IUserMailer UserMailer
        {
            get { return _userMailer; }
            set { _userMailer = value; }
        }
        private NHLdb db = new NHLdb();
        //
        // GET: /Registreer/

        public ActionResult Index()
        {
            return View();
        }

        //
        // Post: /Registreer/
        [HttpPost]
        public ActionResult Index(User user)
        {
            // create a new user and fill in the required fields
            if (Request.Form.Get("ConditionsAccepted") == "false" ) // check if the conditions has been checked
                ModelState.AddModelError("", "De gebruikersvoorwaarden dienen te worden geacepteerd");
            if (Request.Form.Get("PassWord2") != user.Password) // check if password1 and password2 are the same, preventing password typos
                ModelState.AddModelError("", "De ingevulde wachtwoorden komen niet overeen");
            // check if the email is being used 
            var Email = from emailAdress in db.Users
                        where user.Email == emailAdress.Email
                        select emailAdress.Email;
            if (Email.Count() != 0) // email is being used so show an error
                ModelState.AddModelError("", "Het e-mail adress is al gebruikt.");
            // check if the username is being used
            var userName = from NameUser in db.Users
                           where user.UserName == NameUser.UserName
                           select NameUser.UserName;
            if (userName.Count() != 0) // username is being used so show an error
                ModelState.AddModelError("", "Deze username is al gebruikt.");

            if (ModelState.IsValid) // if so add to database
            {
                // everything is right
                // hash the password
                try
                {
                    user.Password = Cryptography.PasswordHash(user.Password);
                    user.ActivationLink = Cryptography.UrlHash(user.Email);
                    db.Users.Add(user);
                    UserMailer.MailConfirm(user.ActivationLink, user.Email).Send(); // .MailConfirm("test")
                }
                catch
                {
                    ModelState.AddModelError("", "Er is iets mis gegaan. Onze excuses. Probeer het opnieuw aub.");
                }
                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                    return RedirectToAction("gelukt");
                }
            }
            // if we got here the fields were incorrect. Reshow the form.
            return View();
        }


        //
        // GET: /registreer/activate
        public ActionResult activeren(string id)
        {
            return View();
        }

        //
        // Post: /Registreer/activate
        [HttpPost]
        public ActionResult activeren(string id, AccountActivation user)
        {
            // query the database for the person with the name of the user that is trying to activated it.
            var passPerson = from userPass in db.Users
                             where userPass.UserName == user.UserName && userPass.Activated == 0 && userPass.ActivationLink == id
                             select userPass;
            if (passPerson.Count() != 1) // Check if there was such a username, else throw error
                ModelState.AddModelError("", "De gebruikersnaam kwam niet overeen met de database.");
            else if(passPerson.First().Password != Cryptography.PasswordHash(user.Password) ) // check if the password did match, else throw error
                ModelState.AddModelError("", "Het wachtwoord kwam niet overeen met de database.");
            if (ModelState.IsValid) 
            {
                // everything was valid. Change the persons state to activated.
                int activatingUserID = passPerson.First().UserID;
                UserMeta newUserMeta = new UserMeta() { UserId = activatingUserID };
                db.UserMeta.Add(newUserMeta);
                passPerson.First().Activated = 1;
                passPerson.First().ActivationLink = null;
                db.SaveChanges();
                return RedirectToAction("activerengelukt", "registreer");
            }
            // if we get here the fields were invalid. Return to the form
            return View();
        }

        //
        // GET: /Registreer/Gelukt
        public ActionResult gelukt()
        {
            return View();
        }

        //
        // GET: /Registreer/ActiverenGelukt
        public ActionResult ActiverenGelukt()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
