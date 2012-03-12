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
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void option should throw an DbEntityValidationException exception")]
        public void EmptyMessage()
        {
            db.Options.Add(new Option {});
            db.SaveChanges();
        }

        [Description("Tests if options that should be valid are valid."), TestCategory("Model.Valid"), TestMethod]
        public void ValidOptions()
        {
            Option o = new Option { Name = "Opt", Value = "True" };
            db.Options.Add(o);
            db.SaveChanges();
        }

        [Description("Tests if a missing value is not valid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving with only a key should throw an error, since there are more fields required")]
        public void InvalidOption1()
        {
            Option o = new Option();
            o.Name = "test";
            db.Options.Add(o);
            db.SaveChanges();
        }

        [Description("Tests if a missing name is not valid."), TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving with only a key should throw an error, since there are more fields required")]
        public void InvalidOption2()
        {
            Option o = new Option();
            o.Value = "This is for a testing purpose.";
            db.Options.Add(o);
            db.SaveChanges();
        }
    }
}
