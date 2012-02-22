using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class FavoriteModelValidationTest
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
        public void EmptyFavorite()
        {
            // Will throw an exception, because some required fields are null;      
            db.Favorites.Add(new Favorite { });
            db.SaveChanges();
        }

        [TestMethod]
        [Description("")]
        public void DefaultForNewFavorite()
        {
            Favorite f = new Favorite();

            // Should not be null
            Assert.IsNotNull(f.Created_At, "Created_At should not be null");

            //Expected Default Values
            Assert.IsTrue(f.Created_At == DateTime.Now.ToString(), "Created_At should be initialized to DateTime.Now.ToString()");
        }

    }
}
