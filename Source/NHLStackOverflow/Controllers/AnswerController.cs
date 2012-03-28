using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{
    public class AnswerController : Controller
    {
        private NHLdb db = new NHLdb();
        
        //
        // GET: /Answer/Delete/AnswerID
        public ActionResult Delete(int id)
        {
            var AnswerDelete = from answerDelete in db.Answers
                               where answerDelete.AnswerID == id
                               select answerDelete;
            int userRank = (from user in db.Users
                            where user.UserName == User.Identity.Name
                            select user.Rank).Single();
            if (AnswerDelete.Count() == 1 && userRank >= 2)
            {
                // delete this awnser
                var AwnserToDelete = AnswerDelete.First();
                db.Answers.Remove(AwnserToDelete);
                var answerVotes = from answerVote in db.Votes
                                  where answerVote.AnswerID == id
                                  select answerVote;
                foreach (var vote in answerVotes)
                    db.Votes.Remove(vote);
                var flags = from flag in db.Flags
                            where flag.AnswerID == id
                            select flag;
                foreach (var flag in flags)
                    db.Flags.Remove(flag);
                // get the comments
                var commentsAwnsers = from comment in db.Comments
                                      where comment.AnswerId == id
                                      select comment;
                // and delete em
                foreach (var comment in commentsAwnsers)
                {
                    db.Comments.Remove(comment);
                    var CommentFlag = from flag in db.Flags
                                      where flag.CommentID == comment.CommentID
                                      select flag;
                    foreach (var comFlag in CommentFlag)
                        db.Flags.Remove(comFlag);
                }
                // lower the amount of answers given by this person with one
                var userMetaAwnser = (from user in db.UserMeta
                                     where user.UserId == AwnserToDelete.UserId
                                     select user).Single();
                userMetaAwnser.Answers -= 1;

                // get some stuff for genarating a message
                var userDeleting = (from usertje in db.Users
                                    where usertje.UserName == User.Identity.Name
                                    select usertje).Single();
                string questionTitle = (from vraag in db.Questions
                                        where vraag.QuestionID == AwnserToDelete.QuestionId
                                        select vraag.Title).Single();
                // Create the message
                Message newMessage = new Message()
                {
                    Title = "Uw antwoord is verwijderd op een vraag",
                    ReceiverId = AwnserToDelete.UserId,
                    SenderId = userDeleting.UserID,
                    Created_At = DateTime.Now.ToString(),
                    Content = "Uw antwoord op de vraag " + questionTitle + " is verwijderd door " + userDeleting.UserName + ". Indien u meer wilt weten over de rede " +
                        "kunt u reageren op dit bericht. Deze word dan verstuurd naar de persoon die hem verwijderd heeft. We wensen u nog een fijne dag."
                };
                // add it to the db
                db.Messages.Add(newMessage);
                // and save
                db.SaveChanges();
            }
            // should return us to the page we were at
            
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //
        // GET: answer/voteup/answerID
        public ActionResult VoteUp(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userVoiting = from userVote in db.Users
                                  where userVote.UserName == User.Identity.Name
                                  select userVote;
                var AnswerGettingVoted = from Answers in db.Answers
                                         where Answers.AnswerID == id
                                         select Answers;

                if (userVoiting.Count() == 1 && AnswerGettingVoted.Count() == 1)
                {
                    var UserVoting = userVoiting.First();
                    // check if this is a up vote or a second time vote (so downvote)
                    var voteInfo = from vote in db.Votes
                                   where vote.UserID == UserVoting.UserID && vote.AnswerID == id
                                   select vote;
                    var answer = AnswerGettingVoted.First();
                    var userGettingVoted = (from user in db.UserMeta
                                           where user.UserId == answer.UserId
                                           select user).Single();
                    if (voteInfo.Count() == 1)
                    {
                        // downvote :<
                        db.Votes.Remove(voteInfo.First());
                        answer.Votes -= 1;
                        userGettingVoted.Votes -= 1;
                        db.SaveChanges();
                    }
                    else
                    {
                        // upvote
                        VoteUser newVote = new VoteUser() { AnswerID = answer.AnswerID, UserID = UserVoting.UserID };
                        answer.Votes += 1;
                        db.Votes.Add(newVote);
                        userGettingVoted.Votes += 1;
                        db.SaveChanges();
                    }
                }
                else
                    ModelState.AddModelError("", "Je account bestaat niet of de vraag bestaat niet.");
            }
            if (!Request.UrlReferrer.AbsolutePath.Contains("vraag"))
                return RedirectToAction("index", "default");
            else
                return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //
        // GET: /answer/FlagAnswer/AnswerID
        public ActionResult FlagAnswer(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var UserFlagging = from usr in db.Users
                                   where usr.UserName == User.Identity.Name
                                   select usr;
                var AnswerFlagged = from answer in db.Answers
                                    where answer.AnswerID == id
                                    select answer;
                if (UserFlagging.Count() == 1 && AnswerFlagged.Count() == 1)
                {
                    var user = UserFlagging.First();
                    var answer = AnswerFlagged.First();
                    var flagged = from flag in db.Flags
                                  where flag.AnswerID == id && flag.UserID == user.UserID
                                  select flag;
                    if (flagged.Count() == 1)
                    {
                        // unflag
                        db.Flags.Remove(flagged.First());
                        db.SaveChanges();
                        var totalFlags = from flags in db.Flags
                                         where flags.AnswerID == id
                                         select flags;
                        if (totalFlags.Count() == 0)
                        {
                            // if there are no longer 
                            answer.Flag = 0;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        // flag
                        answer.Flag = 1;
                        db.Flags.Add(new Flag() { AnswerID = answer.AnswerID, UserID = user.UserID });
                        db.SaveChanges();
                    }
                }
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //
        // GET: /answer/unflag/AnswerID
        // Bestemd om vanuit beheer een antwoord totaal te unflaggen
        public ActionResult Unflag(int id)
        {
            // get question
            var AnswerFlagged = from answer in db.Answers
                                where answer.AnswerID == id && answer.Flag == 1
                                select answer;
            // Check if we are logged in and we are unflagging a right question
            if (User.Identity.IsAuthenticated && AnswerFlagged.Count() == 1)
            {
                // Cast question to single
                var answer = AnswerFlagged.First();
                // get the stuff of our logged in user
                var userUnflagging = (from user in db.Users
                                      where user.UserName == User.Identity.Name
                                      select user).Single();
                // Check the rank
                if (userUnflagging.Rank >= 3)
                {
                    // alowed to delete so delete all the stuff
                    answer.Flag = 0; // set the flag back to 0
                    // Get all the flags of all the people
                    var FlagAnswer = from flag in db.Flags
                                       where flag.AnswerID == answer.AnswerID
                                       select flag;
                    // Remove them all
                    foreach (var flag in FlagAnswer)
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
