using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHLStackOverflow.Models;

namespace NHLStackOverflow.tests.Models
{
    [TestClass]
    public class UserMetaModelValidationTest
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

        /// <summary>
        /// Method which will test the standard values of a MetaData
        /// </summary>
        [TestMethod]
        public void DefaultForNewMetaData()
        {
            UserMeta testMeta = new UserMeta();

            Assert.AreEqual<int>(0, testMeta.AantalAnswers, "Aantal gegeven antwoorden hoort standaard op 0 te staan");
            Assert.AreEqual<int>(0, testMeta.AantalBestAnswers, "Aantal gegeven beste antwoorden hoort standaard op 0 te staan");
            Assert.AreEqual<int>(0, testMeta.AantalQuestions, "Aantal gestelde vragen hoort standaard op 0 te staan");
            Assert.AreEqual<int>(0, testMeta.TotalVotes, "Aantal votes hoort standaard op 0 te staan");
        }

    }
}
