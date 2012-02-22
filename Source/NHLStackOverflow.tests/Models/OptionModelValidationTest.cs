using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class OptionModelValidationTest
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
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException),
            "Saving a void option should throw an exception")]
        public void DefaultForNewOption()
        {
            Option newOption = new Option();
            db.SaveChanges();

        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException),
    "Saving with only a key should throw an error, since there are more fields required")]
        public void TestWithKeyOnly()
        {
            Option newOption = new Option();
            newOption.OptionID = 1;
            db.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException),
    "Saving with only a key should throw an error, since there are more fields required")]
        public void TestWithOnlyValue()
        {
            Option newOption = new Option();
            newOption.Value = "This is for a testing purpose.";
            db.SaveChanges();
        }

    }
}
