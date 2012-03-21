using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Classes;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class UserModelValidationTest
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

        [Description("Tests if the database refuses to store an empty user."), TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void user should throw an DbEntityValidationException exception")]
        public void EmptyUser()
        {
            db.Users.Add(new User { });
            db.SaveChanges();
        }

        [Description("Tests if all the default values for a new user are correct."), TestCategory("Model.Defaults"), TestMethod]
        public void DefaultsForNewUser()
        {
            User u = new User();

            // Test if the constructor writes the correct default values
            Assert.IsNotNull(u.Created_At, "Created At should not be null");
            Assert.IsNotNull(u.LastOnline, "Last online should not be null");
            Assert.IsNotNull(u.Rank, "Rank should not be null");

            // Test if the expected default values are present.
            Assert.IsTrue(u.Created_At == StringToDateTime.ToUnixTimeStamp(DateTime.Now), "Created At should be initialized to DateTime.Now.ToString()");
            Assert.IsTrue(u.LastOnline == u.Created_At, "Last Online should be initialized to be the same as Created At");
           

            // Everything else should be null or 0
            Assert.IsNull(u.UserName, "UserName should be initialized to null");
            Assert.IsNull(u.Password, "Password should be initialized to null");
            Assert.IsNull(u.Email, "Email should be initialized to null");
            Assert.AreEqual<int>(0, u.Rank, "Rank should be defaulted to 0");
            Assert.IsNull(u.Name, "Name should be initialized to null");
            Assert.IsNull(u.Birthday, "Birthday should be initialized to null");
            Assert.IsNull(u.Location, "Location should be initialize to null");
            Assert.IsNull(u.Website, "Website should be initialize to null");
            Assert.IsNull(u.Languages, "Languages should be initialize to null");
        }

        [Description("Test if a missing username is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUser1()
        {
            User u = new User { Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Test if a missing password is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUser2()
        {
            User u = new User { UserName = " Sjakie23", Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if a leading space in a username is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserName1()
        {
            User u = new User { UserName = " Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if a trailing space in a username is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserName2()
        {
            User u = new User { UserName = "Sjakie23 ", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if a space in a username is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserName3()
        {
            User u = new User { UserName = "Sja kie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if a leading underscore in a username is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserName5()
        {
            User u = new User { UserName = "_Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if a leading dash in a username is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserName6()
        {
            User u = new User { UserName = "-Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if special characters in a username is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserName7()
        {
            User u = new User { UserName = "$%$%^#$@#$@#$", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if a username starting with a numerical is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserName8()
        {
            User u = new User { UserName = "1Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if a username shorter then 5 characters is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserName9()
        {
            User u = new User { UserName = "Kort", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if an unhashed password is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserPassword()
        {
            User u = new User { UserName = "Sjakie23", Password = "password1", Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Tests if an missing email is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserEmail1()
        {
            User u = new User { UserName = "Sjakie23", Password = Cryptography.PasswordHash("test") };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Test if an invalid email adress is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserEmail2()
        {
            User u = new User { UserName = "Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Test if an invalid email adress is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserEmail3()
        {
            User u = new User { UserName = "Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Test if an invalid email adress is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserEmail4()
        {
            User u = new User { UserName = "Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Test if an invalid website adress is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserWebsite1()
        {
            User u = new User { Website = "www.google.com", UserName = "Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Test if an invalid website adress is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserWebsite2()
        {
            User u = new User { Website = "ftp://google.com", UserName = "Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Test if an invalid location is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserLocation()
        {
            User u = new User { Location = "leeuwarden", UserName = "Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }

        [Description("Test if a lowercased name is invalid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserRealName()
        {
            User u = new User { Name = "sjaak", UserName = "Sjakie23", Password = Cryptography.PasswordHash("test"), Email = "test@testmail.com" };

            db.Users.Add(u);
            db.SaveChanges();
        }
    }
}