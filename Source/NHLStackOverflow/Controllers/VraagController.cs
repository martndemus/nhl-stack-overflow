using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.Controllers
{
    public class VraagController : Controller
    {
        private NHLdb db = new NHLdb();
        //
        // GET: /Question/

        //public ActionResult Index()
        //{
        //    var TagsList = from tags in db.Tags
        //                   orderby tags.Count descending
        //                   select tags;
        //    ViewBag.TagList = TagsList;

        //    return View();
        //}

        //
        // GET: /Question/detailNum
        public ActionResult View(int id)
        {
            //questiondetails
            var questionDetails = from questionDetail in db.Questions
                                  where questionDetail.QuestionID == id
                                  select questionDetail;
            Question questionDetailView = questionDetails.First();
            ViewBag.QuestionDetail = questionDetailView;

            //tags in sidebar
            var TagsList = from tags in db.Tags
                           orderby tags.Count descending
                           select tags;
            ViewBag.TagList = TagsList;

            List<TagsIDs> abc = new List<TagsIDs>();

            //tags in question
            var TagList = from tagsQuestion in db.Tags
                          join c in db.QuestionTags on tagsQuestion.TagID equals c.TagId
                          where c.QuestionId == id
                          select tagsQuestion;

            foreach (Tag i in TagList)
                abc.Add(new TagsIDs(i, id));
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
                              
            return View();
        }

    }
}
