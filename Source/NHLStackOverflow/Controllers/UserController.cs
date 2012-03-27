using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Classes;
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

        //
        // GET: /User/instellingen
        public ActionResult Instellingen()
        {
            return View();
        }


        //
        // POST: /User/Instellingen
        [HttpPost]
        public ActionResult Instellingen(PassEmail passEmail)
        {
            if (passEmail.NowPassword != null && passEmail.Email == null)
            {
                // going to change the password
                var userInfos = from userInfo in db.Users
                                where userInfo.UserName == User.Identity.Name
                                select userInfo;
                if (userInfos.First().Password != Cryptography.PasswordHash(passEmail.NowPassword))
                    ModelState.AddModelError("", "Het wachtwoord komt nier overeen.");
                if (passEmail.Password1 != passEmail.Password2 || passEmail.Password1 == null)
                    ModelState.AddModelError("", "Het eerste wachtwoord en de tweede kwamen niet overeen.");
                if (ModelState.IsValid)
                {
                    // Allowed for a password change
                    userInfos.First().Password = Cryptography.PasswordHash(passEmail.Password1);
                    db.SaveChanges();
                    ViewBag.Message = "Het wachtwoord is succesvol veranderd.";
                    return View();
                }
            }
            else if (passEmail.Email != null && passEmail.NowPassword == null)
            {
                // going to change the password
                var userInfos = from userInfo in db.Users
                                where userInfo.UserName == User.Identity.Name
                                select userInfo;
                var emailUsers = from emailUser in db.Users
                                 where emailUser.Email == passEmail.Email
                                 select emailUser;
                if (emailUsers.Count() != 0)
                    ModelState.AddModelError("", "Dit email adress is al opgenomen in de database.");
                if (ModelState.IsValid)
                {
                    userInfos.First().Email = passEmail.Email;
                    db.SaveChanges();
                    ViewBag.MessageDuex = "Het email is succesvol veranderd.";
                    return View();
                }
            }
            return View();
        }

        //
        // GET: User/View/UserID
        public ActionResult View(int id)
        {
            var userInfo = from user in db.Users
                       where user.UserID == id
                       select user;
            return View(userInfo.First());

        }

        //
        // GET: User/Admin
        public ActionResult Beheer()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Needed to check if we are allowed to see some bad stuff happening
                var userRanking = from userRank in db.Users
                                  where userRank.UserName == User.Identity.Name
                                  select userRank.Rank;
                ViewBag.Allowed = false; // false if we aren't allowed to check this
                if (userRanking.First() != 0)
                    ViewBag.Allowed = true; // True if we are allowed
                else
                    return View();
                if (userRanking.First() > 0)
                {
                    // Should be allowed to see the bad comments!
                    var badCommets = from comment in db.Comments
                                     where comment.Flag == 1
                                     select comment;
                    ViewBag.badCommentList = badCommets;
                }
                if (userRanking.First() > 1)
                {
                    // Allowed to see bad anwsers that are flagged
                    var badAwnsers = from awnser in db.Answers
                                     where awnser.Flag == 1
                                     select awnser;
                    ViewBag.badAwnserList = badAwnsers;
                }
                if (userRanking.First() > 2)
                {
                    // allowed to see em all, show evil questions aswell
                    var badQuestions = from question in db.Questions
                                       where question.Flag == 1
                                       select question;
                    ViewBag.badQuestionList = badQuestions;
                }
            }
            else
            {
                // Give some error bag somewhere :<
            }
            return View();
        }

        //
        // GET: /user/inbox
        public ActionResult Inbox()
        {
            // get the ID of our current user
            if (User.Identity.IsAuthenticated) // check if we are logged in :D
            {
                // get info from our current user
                var userViewing = from user in db.Users
                                  where user.UserName == User.Identity.Name
                                  select user;
                if (userViewing.Count() == 1)
                {
                    // cast it to an int :D
                    int userID = userViewing.First().UserID;
                    // Get the mails send to this user
                    var mailIn = from mails in db.Messages
                                 where mails.ReceiverId == userID
                                 select mails;

                    // Give em back to the ViewBag
                    ViewBag.MailsIn = mailIn;
                    ViewBag.MailsCount = mailIn.Count();
                }
                else
                    ModelState.AddModelError("", "Er is iets mis gegaan. We hebben geen correcte gebruiker gevonden.");
            }
            else // if not throw error
                ModelState.AddModelError("", "U dient voor deze pagina te zijn ingelogd.");
            return View();
        }

        //
        // GET: /user/viewmai/mailID
        public ActionResult ViewMail(int id)
        {
            // check if we are logged in
            if (!User.Identity.IsAuthenticated)
                ModelState.AddModelError("", "U dient in gelogd te zijn.");
            else
            {
                // get the mail info
                var mail = from mails in db.Messages
                           where mails.MessageID == id
                           select mails;
                // check if the querry returned some mail
                if (mail.Count() != 1)
                    ModelState.AddModelError("", "We hebben geen mail gevonden.");
                else
                {
                    // give it back to our ViewBag :D
                    ViewBag.Mail = mail.First();

                    // And set the flag to viewed ;-)
                    mail.First().Viewed = 1;
                    db.SaveChanges();
                }
            }
            return View();
        }


        //
        // GET: /user/maakbericht/
        public ActionResult MaakBericht()
        {
            return View();
        }

        //
        // POST: /user/maakbericht/
        [HttpPost]
        public ActionResult MaakBericht(Mail berichtje)
        {
            // Kijk of de persoon is ingelogd
            if (!User.Identity.IsAuthenticated)
                ModelState.AddModelError("", "Je moet ingelogd zijn om een berichtje te versturen.");
            // get info of our current user
            var userSending = from userSend in db.Users
                              where userSend.UserName.ToLower() == User.Identity.Name.ToLower()
                              select userSend;
            // get info of the user to send to
            var userTo = from toUser in db.Users
                         where toUser.UserName == berichtje.SendTo
                         select toUser;
            if (userTo.Count() != 1 || berichtje.SendTo == null)
                ModelState.AddModelError("", "De ingevoerde gebruiker bestaat niet.");

            if (ModelState.IsValid)
            {
                Message newMessage = new Message() { SenderId = userSending.First().UserID, Title = berichtje.Title, ReceiverId = userTo.First().UserID, Content = berichtje.Content };
                db.Messages.Add(newMessage);
                db.SaveChanges();
                ViewBag.Message = "Het berichtje is succesvol verstuurd.";
            }
            return View();
        }
    }
}
