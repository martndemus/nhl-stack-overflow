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
        [Description("Tests if saving a void answer will throw an exception")]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void answer should throw an DbEntityValidationException exception")]
        public void EmptyAnswer()
        {        
            
            db.Answers.Add(new Answer {});
            db.SaveChanges();
        }

        [TestMethod]
        [Description("")]
        public void MinimumNewAnswer()
        {
            db.Answers.Add(new Answer { Content = "Test", Created_At = DateTime.Now });
            db.SaveChanges();
        }
    }
}
