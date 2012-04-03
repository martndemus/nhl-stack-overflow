using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.Controllers
{
    public class CommentController : Controller
    {
        NHLdb db = new NHLdb();

        //
        // GET: Comment/Delete/CommentID
        public ActionResult Delete(int id)
        {
            // get the stuff of the comment that has been selected to delete
            var deleteComment = from comment in db.Comments
                          where comment.CommentID == id
                          select comment;
            // check if it gave back a comment if not show an error
            if (deleteComment.Count() != 1)
                ModelState.AddModelError("", "Er is iets mis gegaan. We hebben de comment niet kunnen vinden.");
            // Get the info of the user that hitted delete, check if he exists and has the right rank
            var userAllowed = from user in db.Users
                              where user.UserName == User.Identity.Name && user.Rank > 0
                              select user;
            // check if it gave one user back, if not show an error
            if (userAllowed.Count() != 1)
                ModelState.AddModelError("", "U hebt geen toestemming om een comment te verwijderen.");
            if (ModelState.IsValid)
            {
                // allowed and comment found :D
                // get the flags on this comment
                var commentFlags = from flag in db.Flags
                                   where flag.CommentID == id
                                   select flag;
                // delete each flag
                foreach (var flag in commentFlags)
                    db.Flags.Remove(flag);
                db.Comments.Remove(deleteComment.First());
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //
        // GET: /comment/FlagComment/CommentID
        public ActionResult FlagComment(int id)
        {
            // check if we are logged in
            if (User.Identity.IsAuthenticated)
            {
                // get the info of the user hitting flag
                var UserFlagging = from usr in db.Users
                                   where usr.UserName == User.Identity.Name
                                   select usr;
                // get the info of the comment getting flagged
                var CommentFlagged = from comment in db.Comments
                                     where comment.CommentID == id
                                     select comment;
                // check if each is right
                if (UserFlagging.Count() == 1 && CommentFlagged.Count() == 1)
                {
                    // cast them to a single
                    var user = UserFlagging.First();
                    var comment = CommentFlagged.First();
                    // get the flags on this comment by this user
                    var flagged = from flag in db.Flags
                                  where flag.CommentID == id && flag.UserID == user.UserID
                                  select flag;
                    // if this equals to one then we are unflagging the comment
                    if (flagged.Count() == 1)
                    {
                        // unflag
                        db.Flags.Remove(flagged.First());
                        db.SaveChanges();
                        // get the amount of flags left on this comment
                        var totalFlags = from flags in db.Flags
                                         where flags.CommentID == id
                                         select flags;
                        // if this is 0 after deleting this flag of this user then unflag the comment
                        if (totalFlags.Count() == 0)
                        {
                            comment.Flag = 0;
                            db.SaveChanges();
                        }
                    }
                    else // we are flagging this comment
                    {
                        // flag
                        comment.Flag = 1;
                        // so add a new flag
                        db.Flags.Add(new Flag() { CommentID = comment.CommentID, UserID = user.UserID });
                        db.SaveChanges();
                    }
                }
            }
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        //
        // GET: /comment/unflag/CommentID
        // Bestemd om vanuit beheer een comment totaal te unflaggen
        public ActionResult Unflag(int id)
        {
            // get question
            var CommentFlagged = from comment in db.Comments
                                 where comment.CommentID == id && comment.Flag == 1
                                 select comment;
            // Check if we are logged in and we are unflagging a right question
            if (User.Identity.IsAuthenticated && CommentFlagged.Count() == 1)
            {
                // Cast question to single
                var comment = CommentFlagged.First();
                // get the stuff of our logged in user
                var userUnflagging = (from user in db.Users
                                      where user.UserName == User.Identity.Name
                                      select user).Single();
                // Check the rank
                if (userUnflagging.Rank >= 3)
                {
                    // alowed to delete so delete all the stuff
                    comment.Flag = 0; // set the flag back to 0
                    // Get all the flags of all the people
                    var FlagAnswer = from flag in db.Flags
                                     where flag.CommentID == comment.CommentID
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
