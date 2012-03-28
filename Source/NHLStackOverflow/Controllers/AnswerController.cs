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
                
                // get the comments
                var commentsAwnsers = from comment in db.Comments
                                      where comment.AnswerId == id
                                      select comment;
                // and delete em
                foreach (var comment in commentsAwnsers)
                    db.Comments.Remove(comment);
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
                    if (voteInfo.Count() == 1)
                    {
                        // downvote :<
                        db.Votes.Remove(voteInfo.First());
                        AnswerGettingVoted.First().Votes -= 1;
                        db.SaveChanges();
                    }
                    else
                    {
                        // upvote
                        int AnswerID = AnswerGettingVoted.First().AnswerID;
                        VoteUser newVote = new VoteUser() { AnswerID = AnswerID, UserID = UserVoting.UserID };
                        AnswerGettingVoted.First().Votes += 1;
                        db.Votes.Add(newVote);
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

    }
}
