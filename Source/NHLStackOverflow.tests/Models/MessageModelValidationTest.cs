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
    public class MessageModelValidationTest
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

        [Description("Tests if the database refuses to store an empty message."), TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void message should throw an DbEntityValidationException exception")]
        public void EmptyMessage()
        {
            db.Messages.Add(new Message { });
            db.SaveChanges();
        }

        [Description("Tests if all the default values for a new message are correct."), TestCategory("Model.Defaults"), TestMethod]
        public void DefaultsForMessage()
        {
            Message testMessage = new Message();
            Assert.IsNotNull(testMessage.Created_At, "Datum van aanmaken mag niet null zijn");
            Assert.IsTrue(testMessage.Created_At == StringToDateTime.ToUnixTimeStamp(DateTime.Now), "De Datum zou gelijk moeten zijn aan de DateTime.Now.ToString()");
        }

        [Description("Tests if messages that should be valid are valid."), TestCategory("Model.Valid"), TestMethod]
        public void ValidMessages()
        {
            var validmessages = new List<Message>
            {
                new Message { SenderId = 1, ReceiverId = 2, QuestionId = 1, Title = "This could be a title for a message", Content = "The content for a message could look like this"  },
                new Message { SenderId = 3, ReceiverId = 1, Title = "This could be a title for a message", Content = "The content for a message could look like this" }
            };

            validmessages.ForEach(s => db.Messages.Add(s));
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the receiver id is missing."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid message should throw an DbEntityValidationException exception")]
        public void InvalidMessage1()
        {
            Message m = new Message { SenderId = 3, Title = "This could be a title for a message", Content = "The content for a message could look like this" };

            db.Messages.Add(m);
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the sender id is missing."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid message should throw an DbEntityValidationException exception")]
        public void InvalidMessage2()
        {
            Message m = new Message { ReceiverId = 1, Title = "This could be a title for a message", Content = "The content for a message could look like this" };

            db.Messages.Add(m);
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the title is too short."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid message should throw an DbEntityValidationException exception")]
        public void InvalidMessage3()
        {
            Message m = new Message { SenderId = 1, ReceiverId = 2, QuestionId = 1, Title = "Too short", Content = "The content for a message could look like this" };
                
            db.Messages.Add(m);
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the content for a message is too short"), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid message should throw an DbEntityValidationException exception")]
        public void InvalidMessage4()
        {
            Message m = new Message { SenderId = 1, ReceiverId = 2, QuestionId = 1, Title = "This could be a title for a message", Content = "Too short" };

            db.Messages.Add(m);
            db.SaveChanges();
        }

        [Description("Tests if it's invalid when the title of the message is over 140 characters long."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving invalid message should throw an DbEntityValidationException exception")]
        public void InvalidMessage5()
        {
            Message m = new Message { SenderId = 1, ReceiverId = 2, QuestionId = 1, Title = "This is not a valid title, that is because a title for a message has a maximum of 140 characters, this one has a few more the 140 characters.", Content = "The content for a message could look like this" };

            db.Messages.Add(m);
            db.SaveChanges();
        }
    }
}