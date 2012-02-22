using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class QuestionModelValidationTest
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
        public void EmptyQuestion()
        {
            // Will throw an exception, because some required fields are null;      
            db.Questions.Add(new Question { });
            db.SaveChanges();
        }

        [TestMethod]
        [Description("")]
        public void DefaultsForNewQuestion()
        {
            Question q = new Question();

            // Should not be null
            Assert.IsNotNull(q.Votes, "Votes should not be null");
            Assert.IsNotNull(q.Views, "Views should not be null");
            Assert.IsNotNull(q.Answered, "Answered should not be null");
            Assert.IsNotNull(q.Flag, "Flag should not be null");
            Assert.IsNotNull(q.Created_At, "Created_At should not be null");

            //Expected Default values
            Assert.AreEqual(0, q.Votes, "Votes should be initialized to 0");
            Assert.AreEqual(0, q.Views, "Views should be initialized to 0");
            Assert.AreEqual(0, q.Answered, "Answered should be initialized to 0");
            Assert.AreEqual(0, q.Flag, "Flag should be initialized to 0;");
            Assert.IsTrue(q.Created_At == DateTime.Now.ToString(), "Created At should be initialized to DateTime.Now.ToString()");
            

        }

    }
}
