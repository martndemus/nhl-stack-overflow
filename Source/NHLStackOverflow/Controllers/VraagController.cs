using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Classes;
using NHLStackOverflow.Models;
using NHLStackOverflow.Models.FormDataModels;
using System;

namespace NHLStackOverflow.Controllers
{
    public class VraagController : Controller
    {
        private NHLdb db = new NHLdb();
        //
        // GET: Vraag/View/detailNum
        public ActionResult View(int id)
        {
            var questionDetails = from questionDetail in db.Questions
                                  where questionDetail.QuestionID == id
                                  select questionDetail;
            ViewBag.QuestionDetail = questionDetails.First();
            return View();
        }

        //
        // GET: /Vraag/StelEen
        public ActionResult StelEen()
        {
            ViewBag.ActionName = "steleen";
            ViewBag.ControllerName = "vraag";
            return View();
        }

        //
        // POST: /Vraag/StelEen
        [HttpPost]
        public ActionResult StelEen(string vraag)
        {
            if (vraag == null)
                ModelState.AddModelError("", "Vraag is leeg.");
            if (ModelState.IsValid)
            {
                // For cleaing up the spaces
                char[] toTrim = new char[7] { '?', '.', ',', '!', ':', ':', '/' };
                string vraagTrimmed = vraag.Trim(toTrim);
                vraagTrimmed = vraagTrimmed.ToLower();

                List<Question> questionResultList = new List<Question>();
                // check the database for an existing somewhat simalair question
                string[] toSearch = StringFilter.Trim(vraagTrimmed);
                foreach (string a in toSearch)
                {
                    var questionsFound = from question in db.Questions
                                         where question.Title.Contains(a)
                                         select question;
                    foreach (Question abc in questionsFound)
                        questionResultList.Add(abc);
                }

                ViewBag.SearchResults = from question in questionResultList
                                        group question by question.QuestionID into qsorted
                                        orderby qsorted.Count() descending
                                        select qsorted.First();

                ViewBag.ActionName = "steleen2";
                ViewBag.ControllerName = "vraag";
            }
            return View();
        }

        // 
        // GET: Vraag/StelEen2
        public ActionResult StelEen2(string info)
        {
            ViewBag.Vraag = info;
            return View();
        }

        //
        // POST: Vraag/StelEen2
        [HttpPost]
        public ActionResult StelEen2(Vraag info)
        {
            //
            if (info.vraag == null || info.tags == null || info.content == null)
                ModelState.AddModelError("", "Vul alle velden in aub.");
            else
            {
                // Fields are met hmm let's see if we can find all the tags perhaps :D
                List<string> tagsList = new List<string>(); // new generic list which will contain our tags which we aren't in the database
                string[] tags = info.tags.Split(' '); // assume all tags are splitted by a space (beg)
                foreach (string tag in tags)
                {
                    var temp = from taggies in db.Tags
                               where taggies.Name.Contains(tag)
                               select taggies;
                    if (temp.Count() == 0) // if a tag is in the database the above query should find it. Else it is a new one :D
                        tagsList.Add(tag);
                }

                // get the user information
                var userPosting = from postingUser in db.Users
                                  where postingUser.UserName == User.Identity.Name
                                  join userCombine in db.UserMeta on postingUser.UserID equals userCombine.UserId
                                  select userCombine;
                if (userPosting.Count() != 1)
                {
                    ModelState.AddModelError("", "U bent niet ingelogd."); // aan name dat de join lukt
                    ViewBag.LoggedIn = false;
                }
                // add a check for the info class 
                if ((tagsList.Count() - info.CountTagsSumbitted()) == 0 && ModelState.IsValid)
                {
                    // add the stuff since we are done :D
                    // also check if we have tags to add. If so. Let's add those first (so we can link stuff)
                    for (int i = 0; i < tagsList.Count(); i++)
                    {
                        Tag newTag = new Tag() { Description = info.returnTagContent(i), Name = tagsList.ElementAt(i) };
                        userPosting.First().Tags += 1;
                        db.Tags.Add(newTag);
                    }
                    userPosting.First().Questions += 1;
                    UserMeta userInfo = userPosting.First();
                    Question newQuestion = new Question() { Title = info.vraag, UserId = userInfo.UserId, Content = info.content };
                    db.Questions.Add(newQuestion);
                    db.SaveChanges(); // to make sure that if there were new tags they are added propperly (so we can query the id's right)

                    // query back our last question
                    // should be the first of this list
                    var justAsked = from questionAsked in db.Questions
                                    where questionAsked.UserId == userInfo.UserId
                                    orderby questionAsked.Created_At descending
                                    select questionAsked;
                    // query all the id's needed
                    Question justAskedQuestion = justAsked.First();
                    foreach (string tagje in tags)
                    {
                        // get the id and created an new QuestionTag for it
                        var tagIDPost = from tagIDje in db.Tags
                                        where tagIDje.Name.Contains(tagje)
                                        select tagIDje.TagID;
                        int postTag = tagIDPost.First();
                        QuestionTag newQuestTag = new QuestionTag() { QuestionId = justAskedQuestion.QuestionID, TagId = postTag };
                        db.QuestionTags.Add(newQuestTag);
                    }
                    db.SaveChanges();
                    return RedirectToAction("view", "vraag", new { id = justAskedQuestion.QuestionID });

                    // Now add our question :D
                    // and on done, go to the page showing our question :D
                }
                else
                {
                    // missing tags :<
                    // create a list for the missing tags :<
                    ViewBag.MissingTagList = tagsList;
                    // give allong a counter (since viewbag is doing nasty and won't let me count :<)
                    ViewBag.MissingTagListCount = tagsList.Count();
                }

            }
            return View();
        }
    }
}
