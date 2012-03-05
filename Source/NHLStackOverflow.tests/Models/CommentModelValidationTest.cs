using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class CommentModelValidationTest
    {
        private NHLdb db;

        [TestInitialize]
        public void TestInitialize()
        {
            this.db = new NHLdb();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.db.Dispose();
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void answer should throw an DbEntityValidationException exception")]
        public void EmptyComment()
        {
            // Will throw an exception, because some required fields are null;
            db.Comments.Add(new Comment {});
            db.SaveChanges();
        }

        [TestMethod]
        [Description("")]
        public void DefaultsForNewComment()
        {
            Comment c = new Comment();

            //Should not be null
            Assert.IsNotNull(c.Votes, "Votes should not be null");
            Assert.IsNotNull(c.Created_At, "Created_At should not be null");
            
            //Expected Default Values
            Assert.AreEqual(0, c.Votes, "Votes should be initialized to 0");
            Assert.IsTrue(c.Created_At == DateTime.Now.ToString(), "Created At should be initialized to DateTime.Now.ToString()");
        }

    }
}
