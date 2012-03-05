using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;
using System.Collections.Generic;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class UserModelValidationTest
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
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void user should throw an DbEntityValidationException exception")]
        public void InvalidEmptyUser()
        {
            db.Users.Add(new User { });
            db.SaveChanges();
        }

        [TestMethod]
        public void DefaultsForNewUser()
        {
            User u = new User();

            // Test if the constructor writes the correct default values
            Assert.IsNotNull(u.Created_At, "Created At should not be null");
            Assert.IsNotNull(u.LastOnline, "Last online should not be null");
            Assert.IsNotNull(u.Rank, "Rank should not be null");

            // Test if the expected default values are present.
            Assert.IsTrue(u.Created_At == DateTime.Now.ToString(), "Created At should be initialized to DateTime.Now.ToString()");
            Assert.IsTrue(u.LastOnline == u.Created_At, "Last Online should be initialized to be the same as Created At");
           

            // Everything else should be null or 0
            Assert.IsNull(u.UserName, "UserName should be initialized to null");
            Assert.IsNull(u.Password, "Password should be initialized to null");
            Assert.IsNull(u.Email, "Email should be initialized to null");
            Assert.AreEqual<int>(0, u.Rank, "Rank should be defaulted to 0");
            Assert.IsNull(u.Name, "Name should be initialized to null");
            Assert.AreEqual<int>(0, u.Age, "Age should be defaulted to 0");
            Assert.IsNull(u.Location, "Location should be initialize to null");
            Assert.IsNull(u.Website, "Website should be initialize to null");
            Assert.IsNull(u.Languages, "Languages should be initialize to null");

            // An default user should not have any relations yet.
            //Assert.IsNull(u.Answers, "Answers should be initialize to null");
            //Assert.IsNull(u.Badges, "Badges should be initialize to null");
            //Assert.IsNull(u.Comments, "Comments should be initialize to null");
            //Assert.IsNull(u.Favorites, "Favorites should be initialize to null");
            //Assert.IsNull(u.Messages, "Messages should be initialize to null");
            //Assert.IsNull(u.Questions, "Questions should be initialize to null");
            //Assert.IsNull(u.Read, "Read should be initialize to null");
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "All the following created user should not validate")]
        public void InvalidUserNames()
        {
            // Some invalid usernames
            var users = new List<User>
            {
                // Not start/end with spaces, dashes or underscore
                new User { UserName = " Sjakie23", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "Sjakie23 ", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "-Sjakie23", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "Sjakie23-", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "_Sjakie23", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "Sjakie23_", Password = "password1", Email = "test@testmail.com" },
                
                // No obvious SQL Injection
                new User { UserName = "Sjakie'); DROP TABLE Users;--", Password = "password1", Email = "test@testmail.com" },
                
                // No special characters in usernames
                new User { UserName = "Sjakie&23", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "~Sjakie23", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "Sjakie23***", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "^Sjakie23^", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "Sjak*ie*23", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "Sjak^ie23", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "Sjaki#e23", Password = "password1", Email = "test@testmail.com" },
                new User { UserName = "#Sjakie23", Password = "password1", Email = "test@testmail.com" },
            };

            users.ForEach(s => db.Users.Add(s));
            db.SaveChanges();
        }
    }
}
