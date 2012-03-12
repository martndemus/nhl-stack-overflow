using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class FavoriteModelValidationTest
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

        [Description("Tests if the database refuses to store an empty favorite."), TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void favorite should throw an DbEntityValidationException exception")]
        public void EmptyFavorite()
        {
            // Will throw an exception, because some required fields are null;      
            db.Favorites.Add(new Favorite { });
            db.SaveChanges();
        }

        [TestCategory("Model.Defaults"), TestMethod]
        [Description("Tests if all the default values for a new favorite are correct.")]
        public void DefaultForNewFavorite()
        {
            Favorite f = new Favorite();

            // Should not be null
            Assert.IsNotNull(f.Created_At, "Created_At should not be null");

            //Expected Default Values
            Assert.IsTrue(f.Created_At == DateTime.Now.ToString(), "Created_At should be initialized to DateTime.Now.ToString()");
        }

        [Description("Tests if favorites that should be valid are valid."), TestCategory("Model.Valid"), TestMethod]
        public void ValidFavorites()
        {
            var validfovorites = new List<Favorite>
            {
                new Favorite { UserId = 1, QuestionId = 1 }
            };

            validfovorites.ForEach(s => db.Favorites.Add(s));
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the favorite has no question id. "), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid favorite should throw an DbEntityValidationException exception")]
        public void InvalidFavorite1()
        {
            Favorite f = new Favorite { UserId = 1 };

            db.Favorites.Add(f);
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the favorite has no user id."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid favorite should throw an DbEntityValidationException exception")]
        public void InvalidFavorite2()
        {
            Favorite f = new Favorite { QuestionId = 1 };

            db.Favorites.Add(f);
            db.SaveChanges();
        }
    }
}
