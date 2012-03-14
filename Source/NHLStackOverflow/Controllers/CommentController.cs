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
            var deleteComment = from comment in db.Comments
                          where comment.CommentID == id
                          select comment;
            if (deleteComment.Count() != 1)
                ModelState.AddModelError("", "Er is iets mis gegaan. We hebben de comment niet kunnen vinden.");

            var userAllowed = from user in db.Users
                              where user.UserName == User.Identity.Name && user.Rank > 0
                              select user;
            if (userAllowed.Count() != 1)
                ModelState.AddModelError("", "U hebt geen toestemming om een comment te verwijderen.");
            if (ModelState.IsValid)
            {
                // allowed and post found :D
                // Let's delete it :D
                db.Comments.Remove(deleteComment.First());
                db.SaveChanges();
                return RedirectToAction("admin", "user");
            }
            return View();
        }

    }
}
