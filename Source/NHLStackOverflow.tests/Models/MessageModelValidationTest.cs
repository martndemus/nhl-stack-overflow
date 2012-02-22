using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class MessageModelValidationTest
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
        public void EmptyMessage()
        {
            db.Messages.Add(new Message { });
            db.SaveChanges();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Message testMessage = new Message();
            Assert.IsNotNull(testMessage.Created_At, "Datum van aanmaken mag niet null zijn");
            Assert.IsTrue(testMessage.Created_At == DateTime.Now.ToString(), "De Datum zou gelijk moeten zijn aan de DateTime.Now.ToString()");
        }
    }
}
