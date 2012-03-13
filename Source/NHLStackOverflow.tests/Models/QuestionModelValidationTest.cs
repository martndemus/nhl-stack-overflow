using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;
using NHLStackOverflow.Classes;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class QuestionModelValidationTest
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

        [Description("Tests if the database refuses to store an empty question."), TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void question should throw an DbEntityValidationException exception")]
        public void EmptyQuestion()
        {
            // Will throw an exception, because some required fields are null;      
            db.Questions.Add(new Question { });
            db.SaveChanges();
        }

        [TestCategory("Model.Defaults"), TestMethod]
        [Description("Tests if all the default values for a new question are correct.")]
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
            Assert.IsTrue(q.Created_At == StringToDateTime.ToUnixTimeStamp(DateTime.Now), "Created At should be initialized to DateTime.Now.ToString()");
        }

        [Description("Tests if questions that should be valid are valid."), TestCategory("Model.Valid"), TestMethod]
        public void ValidQuestions()
        {
            var validquestions = new List<Question>
            {
                new Question { UserId = 1, Title = "Hello world!1", Content = "1Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?" },
                new Question { UserId = 2, Votes = 10, Title = "Hello world!2", Content = "2Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?" },
                new Question { UserId = 3, Answered = 1, Title = "Hello world!3", Content = "3Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?" },
                new Question { UserId = 2, Flag = 0, Title = "Hello world!2", Content = "2Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?" }
            };                

            validquestions.ForEach(s => db.Questions.Add(s));
            db.SaveChanges();
        }

        [Description("Tests if a too short title is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid question should throw an DbEntityValidationException exception")]
        public void InvalidQuestion1()
        {
            Question q = new Question { UserId = 1, Title = "Too short", Content = "1Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?" };
                
            db.Questions.Add(q);
            db.SaveChanges();
        }

        [Description("Tests if a title over 140 characters is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid question should throw an DbEntityValidationException exception")]
        public void InvalidQuestion2()
        {
            Question q = new Question { UserId = 1, Title = "This is not a valid title, that is because a title for a question has a maximum of 140 characters, this one has a few more the 140 characters...", Content = "1Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?" };

            db.Questions.Add(q);
            db.SaveChanges();
        }

        [Description("Tests if too short content is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid question should throw an DbEntityValidationException exception")]
        public void InvalidQuestion3()
        {
            Question q = new Question { UserId = 1, Title = "Hello world!", Content = "Too short" };

            db.Questions.Add(q);
            db.SaveChanges();
        }

        [Description("Tests if a flag out of range is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid question should throw an DbEntityValidationException exception")]
        public void InvalidQuestion4()
        {
            Question q = new Question { UserId = 1, Flag = 4, Title = "Hello world!23", Content = "1Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?" };

            db.Questions.Add(q);
            db.SaveChanges();
        }

        [Description("Tests if answered out of range is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid question should throw an DbEntityValidationException exception")]
        public void InvalidQuestion5()
        {
            Question q = new Question { UserId = 1, Answered = 3, Title = "Hello world12!", Content = "1Lorem ipsum text here. So I didn't have to type this. Because I a programmer which are lazies :D. Lorem impsum dor sil ammet. This is an question: Do you work?" };

            db.Questions.Add(q);
            db.SaveChanges();
        }
    }
}
