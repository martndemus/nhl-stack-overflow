using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Controllers
{
    public class AnswerController : Controller
    {
        private NHLdb db = new NHLdb();
        
        //
        // GET: /Answer/Delete/AnswerID
        public ActionResult Delete(int id)
        {
            // Get the infor of the answer which should be deleted
            var AnswerDelete = from answerDelete in db.Answers
                               where answerDelete.AnswerID == id
                               select answerDelete;
            // and get the rank of this user
            int userRank = (from user in db.Users
                            where user.UserName == User.Identity.Name
                            select user.Rank).Single();
            // if we found no answer of the rank is not right skip deleting
            if (AnswerDelete.Count() == 1 && userRank >= 2)
            {
                // delete this awnser
                var AwnserToDelete = AnswerDelete.First();
                // remove the answer
                db.Answers.Remove(AwnserToDelete);
                // get all the votes on it
                var answerVotes = from answerVote in db.Votes
                                  where answerVote.AnswerID == id
                                  select answerVote;
                // loop through it and delete each
                foreach (var vote in answerVotes)
                    db.Votes.Remove(vote);
                // get all the flags on a answer
                var flags = from flag in db.Flags
                            where flag.AnswerID == id
                            select flag;
                // loop through it and delete each
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
                    // get the possible flags on this comment
                    var CommentFlag = from flag in db.Flags
                                      where flag.CommentID == comment.CommentID
                                      select flag;
                    // delete each flag
                    foreach (var comFlag in CommentFlag)
                        db.Flags.Remove(comFlag);
                }
                // lower the amount of answers given by this person with one
                var userMetaAwnser = (from user in db.UserMeta
                                     where user.UserId == AwnserToDelete.UserId
                                     select user).Single();
                userMetaAwnser.Answers -= 1;

                // get some stuff for generating a message
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
                    Created_At = StringToDateTime.ToUnixTimeStamp(DateTime.Now),
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
            // check if we are logged in
            if (User.Identity.IsAuthenticated)
            {
                // if so get the stuff of this user
                var userVoiting = from userVote in db.Users
                                  where userVote.UserName == User.Identity.Name
                                  select userVote;
                // get the stuff of this answer (which gets the vote up or down
                var AnswerGettingVoted = from Answers in db.Answers
                                         where Answers.AnswerID == id
                                         select Answers;
                // check if that did return some stuff
                if (userVoiting.Count() == 1 && AnswerGettingVoted.Count() == 1)
                {
                    // cast it to a single
                    var UserVoting = userVoiting.First();
                    // check if this is a up vote or a second time vote (so downvote)
                    var voteInfo = from vote in db.Votes
                                   where vote.UserID == UserVoting.UserID && vote.AnswerID == id
                                   select vote;
                    // cast the answer to a single
                    var answer = AnswerGettingVoted.First();
                    // get the userMeta of the person whoes answer it is
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
                else // if we didn't find the answer of the account didn't exist
                    ModelState.AddModelError("", "Je account bestaat niet of het antwoord bestaat niet.");
            }
            // then return them back to their page they came from
            if (!Request.UrlReferrer.AbsolutePath.Contains("vraag"))
                return RedirectToAction("index", "default");
            else
                return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //
        // GET: /answer/FlagAnswer/AnswerID
        public ActionResult FlagAnswer(int id)
        {
            // check if we are logged in
            if (User.Identity.IsAuthenticated)
            {
                // get info of the user that is hitting report
                var UserFlagging = from usr in db.Users
                                   where usr.UserName == User.Identity.Name
                                   select usr;
                // get stuff about the answer getting flagged
                var AnswerFlagged = from answer in db.Answers
                                    where answer.AnswerID == id
                                    select answer;
                // check if we got an user and an answer
                if (UserFlagging.Count() == 1 && AnswerFlagged.Count() == 1)
                {
                    // cast the to a single
                    var user = UserFlagging.First();
                    var answer = AnswerFlagged.First();

                    // get info to check if it is a upflag or a down flag
                    var flagged = from flag in db.Flags
                                  where flag.AnswerID == id && flag.UserID == user.UserID
                                  select flag;
                    // if there is a record of it the it is an unflag
                    if (flagged.Count() == 1)
                    {
                        // unflag
                        db.Flags.Remove(flagged.First());
                        db.SaveChanges();
                        // check if the answer still has flags or this was the only one
                        var totalFlags = from flags in db.Flags
                                         where flags.AnswerID == id
                                         select flags;
                        // if no unflag it
                        if (totalFlags.Count() == 0)
                        {
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
            // through us back to the page we came from
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
