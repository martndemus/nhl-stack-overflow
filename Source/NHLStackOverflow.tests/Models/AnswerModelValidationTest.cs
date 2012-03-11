using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class AnswerModelValidationTest
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

        [Description("Tests if the database refuses to store an empty answer."), TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void answer should throw an DbEntityValidationException exception")]
        public void EmptyAnswer()
        {   
            // Will throw an exception, because some required fields are null;      
            db.Answers.Add(new Answer {});
            db.SaveChanges();
        }

        [Description("Tests if all the default values for a new answer are correct."), TestCategory("Model.Defaults"), TestMethod]
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

        [Description("Tests if answers that should be valid are valid."), TestCategory("Model.Valid"), TestMethod]
        public void ValidAnswers()
        {
            var validanswers = new List<Answer>
            {
                new Answer { UserId = 3, QuestionId = 2, Content = "Hello, Here is my fabilous awnser to your somewhat silly question. I've never had problems with this kind of things. I always do the thingy first and afterwards I do the other thingy. Result: Profit. Kthxbai." },
                new Answer { UserId = 1, QuestionId = 3, Content = "Hello, Here is my fabilous awnser to your somewhat silly question. I've never had problems with this kind of things. I always do the thingy first and afterwards I do the other thingy. Result: Profit. Kthxbai.", Flag = 1 }
            };

            validanswers.ForEach(s => db.Answers.Add(s));
            db.SaveChanges();
        }

        [Description("Tests if  it's invalid if the content is too short."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid answers should throw an DbEntityValidationException exception")]
        public void InvalidAnswer1()
        {
            Answer a = new Answer { UserId = 3, QuestionId = 2, Content = "Hello, Im invalid because my content is too short.", Votes = 5 };

            db.Answers.Add(a);
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the flag is out of range."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid answers should throw an DbEntityValidationException exception")]
        public void InvalidAnswer2()
        {
            Answer a = new Answer { UserId = 1, QuestionId = 3, Flag = 5, Content = "Hello, Here is my fabilous awnser to your somewhat silly question. I've never had problems with this kind of things. I always do the thingy first and afterwards I do the other thingy. Result: Profit. Kthxbai.", Votes = 5 };
            
            db.Answers.Add(a);
            db.SaveChanges();
        }
    }
}