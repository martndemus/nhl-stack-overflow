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
            Option o = new Option();
            db.Options.Add(o);
            db.SaveChanges();

        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving with only a key should throw an error, since there are more fields required")]
        public void TestOptionWithKeyOnly()
        {
            Option o = new Option();
            o.Name = "test";
            db.Options.Add(o);
            db.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving with only a key should throw an error, since there are more fields required")]
        public void TestOptionWithOnlyValue()
        {
            Option o = new Option();
            o.Value = "This is for a testing purpose.";
            db.Options.Add(o);
            db.SaveChanges();
        }

    }
}
