using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class ReadModelValidationTest
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
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving a void readModel should throw an DbEntityValidationException exception")]
        public void EmptyReadModel()
        {
            // Will throw an exception, because some required fields are null;      
            db.Read.Add(new Read { });
            db.SaveChanges();
        }

        [TestCategory("Model.Valid"), TestMethod]
        public void ValidRead()
        {
            Read r = new Read { UserId = 3, QuestionId = 5 };
            db.Read.Add(r);
            db.SaveChanges();
        }

        [TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving an invalid readModel should throw an DbEntityValidationException exception")]
        public void InvalidRead1()
        {
            Read r = new Read { UserId = 3 };
            db.Read.Add(r);
            db.SaveChanges();
        }

        [TestCategory("Model.Invalid"), TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException), "Saving an invalid readModel should throw an DbEntityValidationException exception")]
        public void InvalidRead2()
        {
            Read r = new Read { QuestionId = 3 };
            db.Read.Add(r);
            db.SaveChanges();
        }
    }
}
