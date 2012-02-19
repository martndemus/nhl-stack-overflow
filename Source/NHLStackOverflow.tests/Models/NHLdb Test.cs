using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Test_References
{
    /// <summary>
    /// This tests that the Database connection properly initializes and all the models are present.
    /// </summary>
    [TestClass]
    public class NHLdb_Test
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
        public void DB_initializes()
        {
            Assert.IsNotNull(db, "Database context failed to initialize");
        }

        [TestMethod]
        public void ModelsArePresent()
        {
            Assert.IsNotNull(db.Answers, "Answer model does not exist");
            Assert.IsNotNull(db.Badges, "Badges model does not exist");
            Assert.IsNotNull(db.Comments, "Comments model does not exist");
            Assert.IsNotNull(db.Favorites, "Favorites model does not exist");
            Assert.IsNotNull(db.Messages, "Messages model does not exist");
            Assert.IsNotNull(db.Questions, "Questions model does not exist");
            Assert.IsNotNull(db.QuestionTags, "QuestionTags model does not exist");
            Assert.IsNotNull(db.Read, "Read model does not exist");
            Assert.IsNotNull(db.Tags, "Tags model does not exist");
            Assert.IsNotNull(db.Users, "Users model does not exist");
        }
    }
}