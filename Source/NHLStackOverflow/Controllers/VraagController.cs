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
            // check if logged in
            if (User.Identity.IsAuthenticated)
            {
                // give back the rank of this user
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

            // get all the tags on this question
            var TagList = from tagsQuestion in db.Tags
                          join c in db.QuestionTags on tagsQuestion.TagID equals c.TagId
                          where c.QuestionId == id
                          select tagsQuestion;
            // add each to the list
            foreach (Tag i in TagList)
            {
                abc.Add(new TagsIDs(i, id));

                // Sanitize each tag its name
                i.Name = hs.Sanitize(i.Name, HTMLSanitizerOption.StripTags);
            }
            
            ViewBag.Helper = abc;

            // get the user of this question (for the username and link)
            var useringlist = from users in db.Users
                              where users.UserID == questionDetailView.UserId
                              select users;

            ViewBag.QuestionUser = useringlist.First();

            
            // get the comments on this question
            var commentList = from comments in db.Comments
                              orderby comments.Created_At descending
                              where comments.QuestionId == id
                              select comments;
            // add each to the list
            List<Comment> commentUserView = new List<Comment>(commentList);
            ViewBag.CommentsList = commentList;

            // get a list of all the users that made the comment
            List<User> CommentingUsers = new List<User>();
            foreach (Comment commentje in commentUserView)
            {
                var userComment = from commentse in db.Users
                                  where commentse.UserID == commentje.UserId
                                  select commentse;
                CommentingUsers.Add(userComment.First());
            }

            ViewBag.UserCommentList = CommentingUsers;

            // get the answers
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
            // for each answer
            foreach (Answer answertje in answerQuestionView)
            {
                // get the userID (for the username)
                var userAnswer = from answerse in db.Users
                                 where answerse.UserID == answertje.UserId
                                 select answerse;

                AnsweringUsers.Add(userAnswer.First());

                // get al the comments
                var answerCommentList = from qcomments in db.Comments
                                        orderby qcomments.Created_At descending
                                        where qcomments.AnswerId == answertje.AnswerID
                                        select qcomments;

                if (answerCommentList.Count() > 0)
                {
                    // if there are some comments add them
                    foreach (Comment cba in answerCommentList)
                        AnswerComments.Add(cba);
                }
                // foreach comment get the user (for the username again)
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
        // on a post we submitted an comment or an asnwer
        [HttpPost]
        public ActionResult View(int id, CommentAnswer input)
        {
            // check if we are adding a comment on an asnwer
            if (input.awnserComment != null)
            {
                // add a new awnser comment
                if (!Request.IsAuthenticated) // check if we are logged in
                    ModelState.AddModelError("", "U dient te zijn ingelogd om te reageren.");
                if (ModelState.IsValid)
                {
                    // all valid
                    // get the info of the user that is making a comment
                    var userAwnsering = from user in db.Users
                                        where user.UserName == User.Identity.Name
                                        select user;

                    if (userAwnsering.Count() == 1)
                    {
                        // if this yielded some user we can actually create the new comment
                        Comment awnserComment = new Comment() { AnswerId = input.awnserID, Content = input.awnserComment, UserId = userAwnsering.First().UserID };
                        db.Comments.Add(awnserComment);
                        db.SaveChanges();
                    }
                    // return them to the page where this question is at
                    return RedirectToAction("view", "vraag", id);
                }
            }
            else if (input.questionComment != null) // check if we are adding a comment on a question
            {
                // add a new comment to the question
                // check if we are logged in
                if (!Request.IsAuthenticated)
                    ModelState.AddModelError("", "U dient te zijn ingelogd om te reageren.");
                if (ModelState.IsValid)
                {
                    // all valid then get the info of the user which is writing this comment
                    var userAwnsering = from user in db.Users
                                        where user.UserName == User.Identity.Name
                                        select user;
                    if (userAwnsering.Count() == 1) // check if we could find this user
                    {
                        // add the new comment
                        Comment questionComment = new Comment() { UserId = userAwnsering.First().UserID, QuestionId = id, Content = input.questionComment };
                        db.Comments.Add(questionComment);
                        db.SaveChanges();
                    }
                    // go back to the question we came from
                    return RedirectToAction("view", "vraag", id);
                }
            }
            else if (input.awnser != null) // check if we are giving a new asnwer
            {
                // Add a new awnser
                // check if we are logged in
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

                        // get the userID of the person which is giving an asnwer
                        int AwnserUserID = userAwnsering.First().UserID;
                        // mark this question as one which has a new answer
                        thisQuestion.First().Answers += 1;
                        // add the answer
                        Answer questionAwnser = new Answer() { QuestionId = id, UserId = AwnserUserID, Content = input.awnser };
                        db.Answers.Add(questionAwnser);
                        db.SaveChanges();
                        
                        // ###################################
                        //    check if we got a new badge
                        // ###################################
                        if (AnswerBadge.badgeAchieve(AwnserUserID))
                            AnswerBadge.awardBadge(AwnserUserID);
                        if (AnswerCreatorBadge.badgeAchieve(AwnserUserID))
                            AnswerCreatorBadge.awardBadge(AwnserUserID);
                        if (AnswerLordBadge.badgeAchieve(AwnserUserID))
                            AnswerLordBadge.awardBadge(AwnserUserID);
                        // ###################################
                    }
                    // go back to the current question where we got from
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
            // check if there is a question filled in
            if (vraag == null)
                ModelState.AddModelError("", "Vraag is leeg.");
            if (vraag.Length < 10 || vraag.Length > 140) // check if it is of the right lenght
                ModelState.AddModelError("", "De vraag moet minimaal 10 karakters lang zijn en maximaal 140 karakters lang zijn.");
            if (ModelState.IsValid)
            {
                // if all valid goon
                // Take out all the unneeded stuff (for check in there is such a question)
                char[] toTrim = new char[7] { '?', '.', ',', '!', ':', ';', '/' };
                // trim these of the question
                string vraagTrimmed = vraag.Trim(toTrim);
                // and make it lower
                vraagTrimmed = vraagTrimmed.ToLower();

                List<Question> questionResultList = new List<Question>();
                // remove all unneeded words from the string
                string[] toSearch = StringFilter.Trim(vraagTrimmed);
                foreach (string a in toSearch)
                {
                    // now go through each word and check if there is a question containing this word
                    var questionsFound = from question in db.Questions
                                         where question.Title.Contains(a)
                                         select question;
                    // and add each to the list off results
                    foreach (Question abc in questionsFound)
                        questionResultList.Add(abc);
                }

                // give the results back grouped by the amount of times it showed up
                ViewBag.SearchResults = from question in questionResultList
                                        group question by question.QuestionID into qsorted
                                        orderby qsorted.Count() descending
                                        select qsorted.First();

                // change this stuff so we go to the next page this time
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
            // to mark the first time (meaning don't check the tags if there are some new ones)
            bool firstTime = false;
            // check where we came from
            if (Request.UrlReferrer.LocalPath == "/vraag/check")
                firstTime = true; // set the first time if we came from there 
            if (info.vraag == null) // check if the question isn't empty
                ModelState.AddModelError("", "Ga terug en voer een titel voor je vraag in.");
            else if (info.vraag != null && (info.tags == null || info.content == null) && !firstTime) // check if all the fields are filled in
                ModelState.AddModelError("", "Vul alle velden in aub.");
            else if (!firstTime)
            {
                // Fields are met hmm let's see if we can find all the tags perhaps :D
                List<string> tagsList = new List<string>(); // new generic list which will contain our tags which we aren't in the database
                info.tags = info.tags.Trim(',', '!', '?', ':', ';', '.');
                string[] tags = info.tags.Split(' '); // assume all tags are splitted by a space (beg)
                if (tags.Count() > 5)
                    ModelState.AddModelError("", " U mag niet meer dan 5 tags meegeven.");
                List<string> foundTags = new List<string>();
                // check if the given tag exits in the datebase
                foreach (string tag in tags)
                {
                    var temp = from taggies in db.Tags
                               where taggies.Name.Contains(tag)
                               select taggies;
                    // if a tag is in the database the above query should find it. Else it is a new one.
                    if (temp.Count() == 0)  // not found add it to the list of new tags
                        tagsList.Add(tag);
                    else // else mark it as one we have already
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
                // check if we got all new tags filled in and everything is valid
                if ((tagsList.Count() - info.CountTagsSumbitted()) == 0 && ModelState.IsValid)
                {
                    // add the stuff since we are done :D
                    // also check if we have tags to add. If so. Let's add those first (so we can link stuff)
                    int UserID = userPosting.First().UserId;
                    var userPostingMeta = (from usermeta in db.UserMeta
                                           where usermeta.UserId == UserID
                                           select usermeta).Single();

                    // add each new tag to the database
                    for (int i = 0; i < tagsList.Count(); i++)
                    {
                        Tag newTag = new Tag() { Description = info.returnTagContent(i), Name = tagsList.ElementAt(i), Count = 1 };
                        userPostingMeta.Tags += 1;
                        db.Tags.Add(newTag);
                    }
                    // inceremnt the amount of questions that there are with this tag
                    foreach (string tagName in foundTags)
                    {
                        var tagAdd = from tagSelected in db.Tags
                                     where tagSelected.Name.Contains(tagName)
                                     select tagSelected;
                        tagAdd.First().Count += 1;
                    }

                    // add the amount of questions asked by this person with one
                    userPosting.First().Questions += 1;
                    UserMeta userInfo = userPosting.First();
                    // create the question
                    Question newQuestion = new Question() { Title = info.vraag, UserId = userInfo.UserId, Content = info.content };
                    // add it to the db
                    db.Questions.Add(newQuestion);
                    db.SaveChanges(); // to make sure that if there were new tags they are added propperly (so we can query the id's right)

                    // ####################################
                    //     check if we got a new badge
                    // ####################################
                    if (TagBadge.badgeAchieve(userInfo.UserId))
                        TagBadge.awardBadge(userInfo.UserId);
                    if (TagCreatorBadge.badgeAchieve(userInfo.UserId))
                        TagCreatorBadge.awardBadge(userInfo.UserId);
                    if (TagLordBadge.badgeAchieve(userInfo.UserId))
                        TagLordBadge.awardBadge(userInfo.UserId);
                    // ####################################

                    // query back our last question
                    // should be the first of this list
                    var justAsked = from questionAsked in db.Questions
                                    where questionAsked.UserId == userInfo.UserId
                                    orderby questionAsked.Created_At descending
                                    select questionAsked;
                    // query all the id's needed
                    Question justAskedQuestion = justAsked.First();
                    // foreach tag that there in on this question craete a tagID - QuestionID line
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

                    // #################################
                    //    Check if we got a new badge
                    // #################################
                    if (QuestionBadge.badgeAchieve(userInfo.UserId))
                        QuestionBadge.awardBadge(userInfo.UserId);
                    if (QuestionCreatorBadge.badgeAchieve(userInfo.UserId))
                        QuestionCreatorBadge.awardBadge(userInfo.UserId);
                    if (QuestionLordBadge.badgeAchieve(userInfo.UserId))
                        QuestionLordBadge.awardBadge(userInfo.UserId);
                    // #################################

                    // if we get here we are done :D so take us to our just asked question
                    return RedirectToAction("view", "vraag", new { id = justAskedQuestion.QuestionID });
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
            // check if we are logged in
            if (User.Identity.IsAuthenticated)
            {
                // get our rank
                int userRank = (from user in db.Users
                                where user.UserName == User.Identity.Name
                                select user.Rank).Single();
                // get the question which we are trying to delete
                var questionDelete = (from vraag in db.Questions
                                        where vraag.QuestionID == id
                                        select vraag);
                // check if the rank is right and we got a right question
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
                    // get all the flaggs on this question
                    var flags = from flag in db.Flags
                                where flag.QuestionID == id
                                select flag;
                    // delete each
                    foreach (var flag in flags)
                        db.Flags.Remove(flag);

                    // get all the votes on the question
                    var votes = from vote in db.Votes
                                where vote.QuestionID == id
                                select vote;
                    // remove each
                    foreach (var vote in votes)
                        db.Votes.Remove(vote);

                    // go through all the tags linking to this question
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
                        // get all the votes on this answer
                        var answerVotes = from answerVote in db.Votes
                                          where answerVote.AnswerID == antwoordje.AnswerID
                                          select answerVote;
                        // delete those
                        foreach (var vote in answerVotes)
                            db.Votes.Remove(vote);
                        // delete each comment
                        foreach (var deleteComment in commentsOnAwnser)
                        {
                            // get all the flags on this comment
                            var flagsComment = from flagCom in db.Flags
                                               where flagCom.CommentID == deleteComment.CommentID
                                               select flagCom;
                            // delete each comment flag
                            foreach (var flag in flagsComment)
                                db.Flags.Remove(flag);
                            // remove the actual comment
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
                        // get the flags on this question
                        var flagsQuestion = from flagque in db.Flags
                                           where flagque.CommentID == commentQuestion.CommentID
                                           select flagque;
                        // delete those
                        db.Comments.Remove(commentQuestion);
                    }
                    // get the other stuff of the user who is deleting
                    var userDeleting = (from usertje in db.Users
                                       where usertje.UserName == User.Identity.Name
                                       select usertje).Single();
                    // send a message to notify the delete
                    Message newMessage = new Message()
                    {
                        Title = "Een vraag van u is verwijderd",
                        Created_At = StringToDateTime.ToUnixTimeStamp(DateTime.Now),
                        Content = "De volgende vraag is verwijderd:" 
                        + questionToDelete.Title + ". \n\nIndien u vragen heeft over het verwijderen van dit bericht kunt u een reactie op dit bericht versturen. \n\nDit bericht is verwijderd door: " 
                        + userDeleting.UserName + " \n\nWe wensen u nog een fijne dag.", ReceiverId = questionToDelete.UserId, SenderId = userDeleting.UserID };
                    // add the new message to the db
                    db.Messages.Add(newMessage);
                    // save the database
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

        //
        // GET: /vraag/VoteUp/QuestionID
        public ActionResult VoteUp(int id)
        {
            if (User.Identity.IsAuthenticated) // check if logged in
            {
                // get user stuff
                var userVoiting = from userVote in db.Users
                                  where userVote.UserName == User.Identity.Name
                                  select userVote;
                // get the info of the question which is getting the vote
                var QuestionGettingVoted = from vraag in db.Questions
                                           where vraag.QuestionID == id
                                           select vraag;
                // check if they yielded some info (and not more then one)
                if (userVoiting.Count() == 1 && QuestionGettingVoted.Count() == 1)
                {
                    var UserVoting = userVoiting.First();
                    // check if this is a up vote or a second time vote (so downvote)
                    var voteInfo = from vote in db.Votes
                                   where vote.UserID == UserVoting.UserID && vote.QuestionID == id
                                   select vote;
                    // cast this question to a single so we can do somestuff
                    var Question = QuestionGettingVoted.First();
                    // get the info of the person which made this question
                    var userGettingVoted = from user in db.UserMeta
                                           where user.UserId == Question.UserId
                                           select user;
                    if (voteInfo.Count() == 1) // if so we are downvotting
                    {
                        // remove the vote from the datebase
                        db.Votes.Remove(voteInfo.First());
                        Question.Votes -= 1; // lower the votes on this question
                        userGettingVoted.First().Votes -= 1; // and on the user
                        db.SaveChanges(); // save
                    }
                    else // else we are upvoting (firsttime)
                    {
                        // create a new vote
                        VoteUser newVote = new VoteUser() { QuestionID = Question.QuestionID, UserID = UserVoting.UserID };
                        Question.Votes += 1; // increase the votes on this question with one
                        userGettingVoted.First().Votes += 1; // and the user that made this question
                        db.Votes.Add(newVote); // add the vote to the db
                        db.SaveChanges(); // save
                    }
                }
                else
                    ModelState.AddModelError("", "Je account bestaat niet of de vraag bestaat niet.");
            }
            else
                ModelState.AddModelError("", "Je moet ingelogd zijn om te stemmen.");

            // redirect us to the right page
            if (!Request.UrlReferrer.AbsolutePath.Contains("vraag"))
                return RedirectToAction("index", "default");
            else
                return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //
        // GET: vraag/flagvraag/QuestionID
        // om een vraag to flagged als slecht
        public ActionResult FlagVraag(int id)
        {
            // check if we are logged in
            if (User.Identity.IsAuthenticated)
            {
                // get the stuff of the user hitting the flag button
                var UserFlagging = from usr in db.Users
                                   where usr.UserName == User.Identity.Name
                                   select usr;
                // get the stuff of the question getting (un)flagged
                var questionGettingFlagged = from question in db.Questions
                                             where question.QuestionID == id
                                             select question;
                // check if we got some stuff back
                if (UserFlagging.Count() == 1 && questionGettingFlagged.Count() == 1)
                {
                    // cast it to a single
                    var user = UserFlagging.First();
                    var question = questionGettingFlagged.First();

                    // check if we got a flag on this by this user
                    var flagged = from flag in db.Flags
                                  where flag.QuestionID == id && flag.UserID == user.UserID
                                  select flag;
                    if (flagged.Count() == 1) // if so we are unflagging
                    {
                        db.Flags.Remove(flagged.First()); // remove this flag
                        db.SaveChanges();
                        // get the flags left on this question
                        var totalFlags = from flags in db.Flags
                                         where flags.QuestionID == id
                                         select flags;
                        if (totalFlags.Count() == 0) // if this equals 0 then mark the question as not flagged
                        {
                            question.Flag = 0;
                            db.SaveChanges();
                        }
                    }
                    else // else we are flagging a question
                    {
                        question.Flag = 1;
                        // create a new flag
                        db.Flags.Add(new Flag() { QuestionID = question.QuestionID, UserID = user.UserID });
                        db.SaveChanges();
                    }
                }
            }
            // get us back to the page we came from
            if (!Request.UrlReferrer.AbsolutePath.Contains("vraag"))
                return RedirectToAction("index", "default");
            else
                return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //
        // GET: /vraag/unflag/QuestionID
        // Bestemd om vanuit beheer een vraag totaal te unflaggen
        public ActionResult Unflag(int id)
        {
            // get question
            var QuestionFlagged = from vraag in db.Questions
                                  where vraag.QuestionID == id && vraag.Flag == 1
                                  select vraag;
            // Check if we are logged in and we are unflagging a right question
            if (User.Identity.IsAuthenticated && QuestionFlagged.Count() == 1)
            {
                // Cast question to single
                var question = QuestionFlagged.First();
                // get the stuff of our logged in user
                var userUnflagging = (from user in db.Users
                                      where user.UserName == User.Identity.Name
                                      select user).Single();
                // Check the rank
                if (userUnflagging.Rank >= 3)
                {
                    // alowed to delete so delete all the stuff
                    question.Flag = 0; // set the flag back to 0
                    // Get all the flags of all the people
                    var FlagQuestion = from flag in db.Flags
                                       where flag.QuestionID == question.QuestionID
                                       select flag;
                    // Remove them all
                    foreach (var flag in FlagQuestion)
                        db.Flags.Remove(flag);
                    // and save offcourse
                    db.SaveChanges();
                }
            }
            // and return us back to the admin page :D
            return RedirectToAction("beheer", "user");
        }
    }
}
