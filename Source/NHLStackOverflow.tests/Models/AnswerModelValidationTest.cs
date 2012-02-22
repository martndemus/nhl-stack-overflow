using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;
using System.Collections.Generic;
using System;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class AnswerModelValidationTest
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
        public void EmptyAnswer()
        {   
            // Will throw an exception, because some required fields are null;      
            db.Answers.Add(new Answer {});
            db.SaveChanges();
        }

        [TestMethod]
        [Description("")]
        public void DefaultsForNewAnswer()
        {
            Answer a = new Answer();

            // Should not be null
            Assert.IsNotNull(a.Created_At, "Created At should not be null");
            Assert.IsNotNull(a.Flag, "Flag should not be null");
            Assert.IsNotNull(a.Votes, "Votes should not be null");

            // Expected Default values
            Assert.AreEqual(0, a.Flag, "Flag should be initialized to 0");
            Assert.AreEqual(0, a.Votes, "Votes should be initialized to 0");
            Assert.IsTrue(a.Created_At == DateTime.Now.ToString(), "Created At should be initialized to DateTime.Now.ToString()" );
        }
    }
}
