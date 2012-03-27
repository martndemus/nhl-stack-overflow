using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NHLStackOverflow.Classes;
using NHLStackOverflow.Models;
using NHLStackOverflow.Models.FormDataModels;
using System;
using NHLStackOverflow.Models.Badges;

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
            // get the user rank
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserRank = (from user in db.Users
                                    where user.UserName == User.Identity.Name
                                    select user.Rank).Single();
            }
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
            ViewBag.AnswerCount = answerQuestion.Count();
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
                        AnswerComments.Add(cba);
                }

                foreach (Comment qCommentje in answerCommentList)
                {
                    var userQComment = from qcommentse in db.Users
                                       where qcommentse.UserID == qCommentje.UserId
                                       select qcommentse;
                    
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
                    // get the info off the user ansering
                    var userAwnsering = from user in db.Users
                                        where user.UserName == User.Identity.Name
                                        select user;
                    // and about the question
                    var thisQuestion = from questions in db.Questions
                                       where questions.QuestionID == id
                                       select questions;
                    if (userAwnsering.Count() == 1 && thisQuestion.Count() == 1)
                    {

                        // cast the userID to an int to use it again
                        int userID = userAwnsering.First().UserID;
                        // get the usermeta
                        var userAwnseringMeta = (from usermeta in db.UserMeta
                                                where usermeta.UserId == userID
                                                select usermeta).Single();
                        // add one to the amount of answers given
                        userAwnseringMeta.Answers += 1;

                        int AwnserUserID = userAwnsering.First().UserID;

                        thisQuestion.First().Answers += 1;
                        Answer questionAwnser = new Answer() { QuestionId = id, UserId = AwnserUserID, Content = input.awnser };
                        db.Answers.Add(questionAwnser);
                        var awnseringMeta = (from user in db.UserMeta
                                            where user.UserId == AwnserUserID
                                            select user).Single();
                        awnseringMeta.Answers += 1;
                        db.SaveChanges();
                        if (AnswerBadge.badgeAchieve(AwnserUserID))
                            AnswerBadge.awardBadge(AwnserUserID);
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
            if (vraag.Length < 10 || vraag.Length > 140)
                ModelState.AddModelError("", "De vraag moet minimaal 10 karakters lang zijn en maximaal 140 karakters lang zijn.");
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
                    if (TagBadge.badgeAchieve(userInfo.UserId))
                        TagBadge.awardBadge(userInfo.UserId);

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
                    if (QuestionBadge.badgeAchieve(userInfo.UserId))
                        QuestionBadge.awardBadge(userInfo.UserId);
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

        //
        // GET: /Vraag/Delete/ID
        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                int userRank = (from user in db.Users
                                where user.UserName == User.Identity.Name
                                select user.Rank).Single();
                var questionDelete = (from vraag in db.Questions
                                        where vraag.QuestionID == id
                                        select vraag);
                if (userRank >= 3 && questionDelete.Count() == 1)
                {
                    // Allowed to delete a question
                    // so let's select the question
                    var questionToDelete = questionDelete.First();
                    // let's select the person who made this question
                    var userMetaPoster = (from userMeta in db.UserMeta
                                          where userMeta.UserId == questionToDelete.UserId
                                          select userMeta).Single();
                    // lower the amount of questions asked by this person with one
                    userMetaPoster.Questions -= 1;
                    // delete the question
                    db.Questions.Remove(questionToDelete);
                    // get all the question tags which were linked with this question
                    var questionTagsDelete = from questionTag in db.QuestionTags
                                             where questionTag.QuestionId == id
                                             select questionTag;
                    // loop through them
                    foreach (var vraagTag in questionTagsDelete)
                    {
                        // get the tag of this question
                        var tags = (from tagjes in db.Tags
                                   where tagjes.TagID == vraagTag.TagId
                                   select tagjes).Single();
                        // lower the amount of questions in this tag with one
                        tags.Count -= 1;
                        // remove this QuestionTag
                        db.QuestionTags.Remove(vraagTag);
                    }
                    // get all the awnsers given to this question
                    var antwoorden = from antwoord in db.Answers
                                     where antwoord.QuestionId == id
                                     select antwoord;
                    // Loopt through them
                    foreach (var antwoordje in antwoorden)
                    {
                        // get all the comments on this awnser
                        var commentsOnAwnser = from comments in db.Comments
                                               where comments.AnswerId == antwoordje.AnswerID
                                               select comments;
                        // delete each comment
                        foreach (var deleteComment in commentsOnAwnser)
                        {
                            db.Comments.Remove(deleteComment);
                        }
                        // get the stuff of the person who made this awnser
                        var antwoordMeta = (from metaAntwoord in db.UserMeta
                                            where metaAntwoord.UserId == antwoordje.UserId
                                            select metaAntwoord).Single();
                        // lower the amount of awnser of this person with one
                        antwoordMeta.Answers -= 1;
                        // remove the awnser
                        db.Answers.Remove(antwoordje);
                    }
                    // Get all the comments on this question
                    var questionComments = from deleteQuestionComment in db.Comments
                                           where deleteQuestionComment.QuestionId == id
                                           select deleteQuestionComment;
                    // Remove each one of them
                    foreach (var commentQuestion in questionComments)
                    {
                        db.Comments.Remove(commentQuestion);
                    }
                    // get the other stuff of the user who is deleting
                    var userDeleting = (from usertje in db.Users
                                       where usertje.UserName == User.Identity.Name
                                       select usertje).Single();
                    // send a message to notify the delete
                    Message newMessage = new Message() { Title = "Een vraag van u is verwijderd", Created_At = DateTime.Now.ToString(), Content = "De volgende vraag is verwijderd:" 
                        + questionToDelete.Title + ". \n\nIndien u vragen heeft over het verwijderen van dit bericht kunt u een reactie op dit bericht versturen. \n\nDit bericht is verwijderd door: " 
                        + userDeleting.UserName + " \n\nWe wensen u nog een fijne dag.", ReceiverId = questionToDelete.UserId, SenderId = userDeleting.UserID };
                    // save the changes
                    db.Messages.Add(newMessage);
                    db.SaveChanges();
                    // show a succes message
                    ViewBag.Message = "De vraag is succesvol verwijderd.";
                }
                else
                {
                    ViewBag.Message = "U mag geen vragen verwijderen of de vraag bestaat niet.";
                }

            }
            else
            {
                ViewBag.Message = "U bent niet ingelogd";
            }
            if (Request.UrlReferrer.AbsolutePath.Contains("vraag"))
                return RedirectToAction("index", "default");
            else
                return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}
