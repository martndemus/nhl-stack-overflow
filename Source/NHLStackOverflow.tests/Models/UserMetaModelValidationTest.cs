using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class UserMetaModelValidationTest
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

        [Description("Tests if the database refuses to store an empty usermeta."), TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void usermeta should throw an DbEntityValidationException exception")]
        public void EmptyUserMeta()
        {
            // Will throw an exception, because some required fields are null;      
            db.UserMeta.Add(new UserMeta { });
            db.SaveChanges();
        }

        /// <summary>
        /// Method which will test the standard values of a MetaData
        /// </summary>
        [Description("Tests if all the default values for a new usermeta are correct."), TestCategory("Model.Defaults"), TestMethod]
        public void DefaultForNewMetaData()
        {
            UserMeta u = new UserMeta();

            Assert.AreEqual<int>(0, u.Answers, "Aantal gegeven antwoorden hoort standaard op 0 te staan");
            Assert.AreEqual<int>(0, u.BestAnswers, "Aantal gegeven beste antwoorden hoort standaard op 0 te staan");
            Assert.AreEqual<int>(0, u.Questions, "Aantal gestelde vragen hoort standaard op 0 te staan");
            Assert.AreEqual<int>(0, u.Votes, "Aantal votes hoort standaard op 0 te staan");
            Assert.AreEqual<int>(0, u.Tags, "Aantal tags hoort standaard op 0 te staan");
        }

        [Description("Tests if usermeta that should be valid are valid."), TestCategory("Model.Valid"), TestMethod]
        public void ValidUserMeta()
        {
            var validusermeta = new List<UserMeta>
            {
                new UserMeta { Questions = 10, BestAnswers = 1, Votes = 25, Answers = 4, Tags = 5, UserId = 2 },
                new UserMeta { BestAnswers = 1, Votes = 25, Answers = 4, Tags = 5, UserId = 2 },
                new UserMeta { Questions = 10, Votes = 25, Answers = 4, Tags = 5, UserId = 2 },
                new UserMeta { Questions = 10, BestAnswers = 1, Answers = 4, Tags = 5, UserId = 2 },
                new UserMeta { Questions = 10, BestAnswers = 1, Votes = 25, Tags = 5, UserId = 2 },
                new UserMeta { Questions = 10, BestAnswers = 1, Votes = 25, Answers = 4, UserId = 2 }
            };

            validusermeta.ForEach(s => db.UserMeta.Add(s));
            db.SaveChanges();
        }

        [Description("Tests if a missing user id is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving an invalid usermeta should throw an DbEntityValidationException exception")]
        public void InvaliduserMeta()
        {
            UserMeta u = new UserMeta { Questions = 10, BestAnswers = 1, Votes = 25, Answers = 4, Tags = 5 };

            db.UserMeta.Add(u);
            db.SaveChanges();
        }
    }
}
