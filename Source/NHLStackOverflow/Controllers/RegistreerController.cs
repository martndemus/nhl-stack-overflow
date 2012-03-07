using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.FormDataModels;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Controllers
{
    public class RegistreerController : Controller
    {
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
            {
                ModelState.AddModelError("", "De gebruikersvoorwaarden dienen te worden geacepteerd");
            }
            if (Request.Form.Get("PassWord2") != user.Password) // check if password1 and password2 are the same, preventing password typos
            {
                ModelState.AddModelError("", "De ingevulde wachtwoorden komen niet overeen");
            }
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
                user.Password = PasswordHasher.Hash(user.Password);
                db.Users.Add(user);
                db.SaveChanges();
            }
            // if we got here the fields were incorrect. Reshow the form.
            return View();
        }
    }
}
