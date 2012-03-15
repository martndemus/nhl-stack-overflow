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
                if (userInfos.First().Password != PasswordHasher.Hash(passEmail.NowPassword))
                    ModelState.AddModelError("", "Het wachtwoord komt nier overeen.");
                if (passEmail.Password1 != passEmail.Password2 || passEmail.Password1 == null)
                    ModelState.AddModelError("", "Het eerste wachtwoord en de tweede kwamen niet overeen.");
                if (ModelState.IsValid)
                {
                    // Allowed for a password change
                    userInfos.First().Password = PasswordHasher.Hash(passEmail.Password1);
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
        public ActionResult Admin()
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

            return View();
        }
    }
}
