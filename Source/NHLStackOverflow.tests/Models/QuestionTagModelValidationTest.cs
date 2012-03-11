﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class QuestionTagModelValidationTest
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

        [TestCategory("Model.Empty"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void tag should throw an DbEntityValidationException exception")]
        public void EmptyQuestionTag()
        {
            // Will throw an exception, because some required fields are null;      
            db.QuestionTags.Add(new QuestionTag { });
            db.SaveChanges();
        }

        [TestCategory("Model.Valid"), TestMethod]
        public void ValidQuestionTag()
        {
            QuestionTag q = new QuestionTag { QuestionId = 1, TagId = 3 };
            db.QuestionTags.Add(q);
            db.SaveChanges();
        }

        [TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving an invalid tag should throw an DbEntityValidationException exception")]
        public void InvalidQuestionTag1()
        {
            QuestionTag q = new QuestionTag { QuestionId = 1 };
            db.QuestionTags.Add(q);
            db.SaveChanges();
        }

        [TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving an invalid tag should throw an DbEntityValidationException exception")]
        public void InvalidQuestionTag()
        {
            QuestionTag q = new QuestionTag { TagId = 3 };
            db.QuestionTags.Add(q);
            db.SaveChanges();
        }
    }
}
