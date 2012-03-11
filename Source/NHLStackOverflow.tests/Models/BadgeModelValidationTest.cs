using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class BadgeModelValidationTest
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

        [TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void badge should throw an DbEntityValidationException exception")]
        public void EmptyBadge()
        {
            //will throw an exception, because some required fields are null
            db.Badges.Add(new Badge {});
            db.SaveChanges();
        }

        [TestCategory("Model.Defaults"), TestMethod]
        [Description("")]
        public void DefaultsForNewBadge()
        {
            Badge b = new Badge();

            //should not be null
            Assert.IsNotNull(b.Created_At, "Created_At should not be null");
            
            //Expected Default values
            Assert.IsTrue(b.Created_At == DateTime.Now.ToString(), "Created At should be initialized to DateTime.Now.ToString()");
        }

        [TestCategory("Model.Valid"), TestMethod]
        public void ValidBadges()
        {
            var validbadges = new List<Badge>
            {
                new Badge { UserId = 1, Name = "100BestAnswers"}
            };

            validbadges.ForEach(s => db.Badges.Add(s));
            db.SaveChanges();
        }

        [TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid badges should throw an DbEntityValidationException exception")]
        public void InvalidBadge1()
        {
            Badge b = new Badge { Name = "", UserId = 1 };

            db.Badges.Add(b);
            db.SaveChanges();
        }

        [TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid badges should throw an DbEntityValidationException exception")]
        public void InvalidBadge2()
        {
            Badge b = new Badge { UserId = 1 };

            db.Badges.Add(b);
            db.SaveChanges();
        }

        [TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid badges should throw an DbEntityValidationException exception")]
        public void InvalidBadge3()
        {
            Badge b = new Badge { Name = "BestAnswer" };

            db.Badges.Add(b);
            db.SaveChanges();
        }
    }
}
