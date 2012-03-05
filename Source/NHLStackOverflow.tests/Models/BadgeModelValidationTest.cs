using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class BadgeModelValidationTest
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
        public void EmptyBadge()
        {
            //will throw an exception, because some required fields are null
            db.Badges.Add(new Badge {});
            db.SaveChanges();
        }

        [TestMethod]
        [Description("")]
        public void DefaultsForNewBadge()
        {
            Badge b = new Badge();

            //should not be null
            Assert.IsNotNull(b.Created_At, "Created_At should not be null");
            
            //Expected Default values
            Assert.IsTrue(b.Created_At == DateTime.Now.ToString(), "Created At should be initialized to DateTime.Now.ToString()");

        }


    }
}
