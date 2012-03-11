using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class TagModelValidationTest
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

        [Description("Tests if the database refuses to store an empty tag."), TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void tag should throw an DbEntityValidationException exception")]
        public void EmptyTagModel()
        {
            // Will throw an exception, because some required fields are null;      
            db.Tags.Add(new Tag { });
            db.SaveChanges();
        }

        [Description("Tests if tags that should be valid are valid."), TestCategory("Model.Valid"), TestMethod]
        public void ValidTag()
        {
            Tag t = new Tag { Name = "C#", Description = "Ewwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww", Count = 3234 };
            db.Tags.Add(t);
            db.SaveChanges();
        }

        [Description("Tests if a too short description is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving an invalid tag should throw an DbEntityValidationException exception")]
        public void InvalidTag1()
        {
            Tag t = new Tag { Name = "C#", Description = "Too short", Count = 3234 };
            db.Tags.Add(t);
            db.SaveChanges();
        }

        [Description("Tests if an empty name is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving an invalid tag should throw an DbEntityValidationException exception")]
        public void InvalidTag2()
        {
            Tag t = new Tag { Name = "", Description = "A short description of a tag", Count = 3234 };
            db.Tags.Add(t);
            db.SaveChanges();
        }

        [Description("Tests if a missing count is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving an invalid tag should throw an DbEntityValidationException exception")]
        public void InvalidTag3()
        {
            Tag t = new Tag { Name = "Ruby", Description = "A short description of a tag" };
            db.Tags.Add(t);
            db.SaveChanges();
        }
    }
}
