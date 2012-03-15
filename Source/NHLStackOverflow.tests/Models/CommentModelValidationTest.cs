using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class CommentModelValidationTest
    {
        private static NHLdb db;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            db = new NHLdb();
            db.Database.Initialize(true);
            db.Dispose();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            db.Database.Delete();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            db = new NHLdb();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            db.Dispose();
        }

        [Description("Tests if the database refuses to store an empty comment."), TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void comment should throw an DbEntityValidationException exception")]
        public void EmptyComment()
        {
            // Will throw an exception, because some required fields are null;
            db.Comments.Add(new Comment {});
            db.SaveChanges();
        }

        [TestCategory("Model.Defaults"), TestMethod]
        [Description("Tests if all the default values for a new comment are correct.")]
        public void DefaultsForNewComment()
        {
            Comment c = new Comment();

            //Should not be null
            Assert.IsNotNull(c.Votes, "Votes should not be null");
            Assert.IsNotNull(c.Created_At, "Created_At should not be null");
            
            //Expected Default Values
            Assert.AreEqual(0, c.Votes, "Votes should be initialized to 0");
            Assert.IsTrue(c.Created_At == StringToDateTime.ToUnixTimeStamp(DateTime.Now), "Created At should be initialized to DateTime.Now.ToString()");
        }

        [Description("Tests if comments that should be valid are valid."), TestCategory("Model.Valid"), TestMethod]
        public void ValidComment()
        {
            var validcomments = new List<Comment>
            {
                new Comment { UserId = 1, QuestionId = 5, Content = "This is a comment on a question", Votes = 1},
                new Comment { UserId = 2, CommentID = 3, Content = "This is a comment on a question"}
            };

            validcomments.ForEach(s => db.Comments.Add(s));
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the comment has both a question and answer id."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid comment should throw an DbEntityValidationException exception")]
        public void Invalidcomment1()
        {
            Comment c = new Comment { UserId = 1, QuestionId = 1, CommentID = 2, Content = "This is a comment on a question" };

            db.Comments.Add(c);
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the content is less then 10 characters long."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid comment should throw an DbEntityValidationException exception")]
        public void Invalidcomment2()
        {
            Comment c = new Comment { UserId = 1, QuestionId = 1, Content = "Too short" };

            db.Comments.Add(c);
            db.SaveChanges();
        }
    }
}