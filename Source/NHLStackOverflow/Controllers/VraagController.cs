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
            HTMLSanitizer hs = new HTMLSanitizer();
            Markdown md = new Markdown();

            //questiondetails
            var questionDetails = from questionDetail in db.Questions
                                  where questionDetail.QuestionID == id
                                  select questionDetail;
            questionDetails.First().Views += 1;
            db.SaveChanges();

            Question questionDetailView = questionDetails.First();
            
            // Sanitize the post content and title
            // Then process the content with Markdown
            questionDetailView.Title = hs.EscapeHTMLentities(questionDetailView.Title);
            questionDetailView.Content = hs.Sanitize(questionDetailView.Content, HTMLSanitizerOption.UnescapeMarkDown);
            questionDetailView.Content = md.Transform(questionDetailView.Content);

            ViewBag.QuestionDetail = questionDetailView;

            List<TagsIDs> abc = new List<TagsIDs>();

            //tags in question
            var TagList = from tagsQuestion in db.Tags
                          join c in db.QuestionTags on tagsQuestion.TagID equals c.TagId
                          where c.QuestionId == id
                          select tagsQuestion;

            foreach (Tag i in TagList)
            {
                abc.Add(new TagsIDs(i, id));

                // Sanitize each tag its name
                i.Name = hs.Sanitize(i.Name, HTMLSanitizerOption.StripTags);
            }
            
            ViewBag.Helper = abc;

            //user verificatie
            var useringlist = from users in db.Users
                              where users.UserID == questionDetailView.UserId
                              select users;

            ViewBag.QuestionUser = useringlist.First();

            
            //comments
            var commentList = from comments in db.Comments
                              orderby comments.Created_At descending
                              where comments.QuestionId == id
                              select comments;

            List<Comment> commentUserView = new List<Comment>(commentList);
            ViewBag.CommentsList = commentList;


            List<User> CommentingUsers = new List<User>();
            foreach (Comment commentje in commentUserView)
            {
                var userComment = from commentse in db.Users
                                  where commentse.UserID == commentje.UserId
                                  select commentse;
                CommentingUsers.Add(userComment.First());
            }

            ViewBag.UserCommentList = CommentingUsers;

            var answerQuestion = from answers in db.Answers
                                 orderby answers.Created_At descending
                                 where answers.QuestionId == id
                                 select answers;
            List<Answer> answerQuestionView = new List<Answer>(answerQuestion);
            ViewBag.AnswerQuestionList = answerQuestion;

            // Set the count to 'n' or 'geen' if there are no answers yet
            ViewBag.AnswerQuestionCount = answerQuestion.Count() > 0 ? answerQuestion.Count().ToString() : "geen" ;

            List<User> AnsweringUsers = new List<User>();
            List<Comment> AnswerComments = new List<Comment>();
            List<User> qCommentUsers = new List<User>();
            
            foreach (Answer answertje in answerQuestionView)
            {
                var userAnswer = from answerse in db.Users
                                 where answerse.UserID == answertje.UserId
                                 select answerse;

                AnsweringUsers.Add(userAnswer.First());

                var answerCommentList = from qcomments in db.Comments
                                        orderby qcomments.Created_At descending
                                        where qcomments.AnswerId == answertje.AnswerID
                                        select qcomments;

                if (answerCommentList.Count() > 0)
                {
                    foreach (Comment cba in answerCommentList)
                    {
                        // Sanitize the content of the comment.
                        cba.Content = hs.EscapeHTMLentities(cba.Content);

                        // ????
                        AnswerComments.Add(cba);
                    }
                }

                foreach (Comment qCommentje in answerCommentList)
                {
                    var userQComment = from qcommentse in db.Users
                                       where qcommentse.UserID == qCommentje.UserId
                                       select qcommentse;

                    
                    // Sanitize the content of the comment.
                    qCommentje.Content = hs.EscapeHTMLentities(qCommentje.Content);

                    qCommentUsers.Add(userQComment.First());
                }

                // Sanitize the content of the answer and process it with Markdown
                answertje.Content = hs.Sanitize(answertje.Content, HTMLSanitizerOption.UnescapeMarkDown);
                answertje.Content = md.Transform(answertje.Content);
            }
            ViewBag.UserAnswerList = AnsweringUsers;
            ViewBag.AnswerComments = AnswerComments;
            ViewBag.qCommentList = qCommentUsers;
      
            return View();
        }

        //
        // POST: /Vraag/View/QuestionID
        [HttpPost]
        public ActionResult View(int id, CommentAnswer input)
        {
            if (input.awnserComment != null)
            {
                // add a new awnser comment
                if (!Request.IsAuthenticated)
                    ModelState.AddModelError("", "U dient te zijn ingelogd om te reageren.");
                if (ModelState.IsValid)
                {
                    var userAwnsering = from user in db.Users
                                        where user.UserName == User.Identity.Name
                                        select user;

                    if (userAwnsering.Count() == 1)
                    {
                        Comment awnserComment = new Comment() { AnswerId = input.awnserID, Content = input.awnserComment, UserId = userAwnsering.First().UserID };
                        db.Comments.Add(awnserComment);
                        db.SaveChanges();
                    }
                    return RedirectToAction("view", "vraag", id);
                }
            }
            else if (input.questionComment != null)
            {
                // add a new comment to the question
                if (!Request.IsAuthenticated)
                    ModelState.AddModelError("", "U dient te zijn ingelogd om te reageren.");
                if (ModelState.IsValid)
                {
                    var userAwnsering = from user in db.Users
                                        where user.UserName == User.Identity.Name
                                        select user;
                    if (userAwnsering.Count() == 1)
                    {
                        Comment questionComment = new Comment() { UserId = userAwnsering.First().UserID, QuestionId = id, Content = input.questionComment };
                        db.Comments.Add(questionComment);
                        db.SaveChanges();
                    }
                    return RedirectToAction("view", "vraag", id);
                }
            }
            else if (input.awnser != null)
            {
               // Add a new awnser
                // add a new comment to the question
                if (!Request.IsAuthenticated)
                    ModelState.AddModelError("", "U dient te zijn ingelogd om te reageren.");
                if (ModelState.IsValid)
                {
                    var userAwnsering = from user in db.Users
                                        where user.UserName == User.Identity.Name
                                        select user;
                    var thisQuestion = from questions in db.Questions
                                       where questions.QuestionID == id
                                       select questions;
                    if (userAwnsering.Count() == 1 && thisQuestion.Count() == 1)
                    {
                        thisQuestion.First().Answers += 1;
                        Answer questionAwnser = new Answer() { QuestionId = id, UserId = userAwnsering.First().UserID, Content = input.awnser };
                        db.Answers.Add(questionAwnser);
                        db.SaveChanges();
                    }
                    return RedirectToAction("view", "vraag", id);
                }

            }
            return View(id);
        }
        //
        // GET: /Vraag/StelEen
        public ActionResult Check()
        {
            ViewBag.ActionName = "check";
            ViewBag.ControllerName = "vraag";
            return View();
        }

        //
        // POST: /Vraag/StelEen
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)] 
        public ActionResult Check(string vraag)
        {
            if (vraag == null)
                ModelState.AddModelError("", "Vraag is leeg.");
            if (ModelState.IsValid)
            {
                // For cleaing up the spaces
                char[] toTrim = new char[7] { '?', '.', ',', '!', ':', ';', '/' };
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

                ViewBag.ActionName = "nieuw";
                ViewBag.ControllerName = "vraag";
            }
            return View();
        }

        // 
        // GET: Vraag/Nieuw
        public ActionResult Nieuw(string info)
        {
            ViewBag.Vraag = info;
            return View();
        }

        //
        // POST: Vraag/Nieuw
        [HttpPost]
        public ActionResult Nieuw(Vraag info)
        {
            HTMLSanitizer hs = new HTMLSanitizer();
            Markdown md = new Markdown();

            bool firstTime = false;
            if (Request.UrlReferrer.LocalPath == "/vraag/check")
                firstTime = true;
            if (info.vraag == null)
                ModelState.AddModelError("", "Ga terug en voer een titel voor je vraag in.");
            else if (info.vraag != null && (info.tags == null || info.content == null) && !firstTime)
                ModelState.AddModelError("", "Vul alle velden in aub.");
            else if (!firstTime)
            {
                // Fields are met hmm let's see if we can find all the tags perhaps :D
                List<string> tagsList = new List<string>(); // new generic list which will contain our tags which we aren't in the database
                info.tags = info.tags.Trim(',', '!', '?', ':', ';', '.');
                string[] tags = info.tags.Split(' '); // assume all tags are splitted by a space (beg)
                List<string> foundTags = new List<string>();
                foreach (string tag in tags)
                {
                    var temp = from taggies in db.Tags
                               where taggies.Name.Contains(tag)
                               select taggies;
                    if (temp.Count() == 0) // if a tag is in the database the above query should find it. Else it is a new one :D
                        tagsList.Add(tag);
                    else
                        foundTags.Add(tag);
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
                        Tag newTag = new Tag() { Description = info.returnTagContent(i), Name = tagsList.ElementAt(i), Count = 1 };
                        userPosting.First().Tags += 1;
                        db.Tags.Add(newTag);
                    }
                    foreach (string tagName in foundTags)
                    {
                        var tagAdd = from tagSelected in db.Tags
                                     where tagSelected.Name.Contains(tagName)
                                     select tagSelected;
                        tagAdd.First().Count += 1;
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
