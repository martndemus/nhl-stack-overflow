using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHLStackOverflow.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NHLStackOverflow.tests.Classes
{
    [TestClass]
    public class HTMLSanitizerTest
    {
        private HTMLSanitizer hs;

        [TestInitialize]
        public void TestInitialize()
        {
            hs = new HTMLSanitizer();
        }

        [TestMethod]
        public void TestStripTags()
        {
            Assert.AreEqual(hs.StripTags("<script>"), string.Empty);
            Assert.AreEqual(hs.StripTags("</script>"), string.Empty);
            Assert.AreEqual(hs.StripTags("<a href=\"testc\">"), string.Empty);            
        }

        [TestMethod]
        public void TestSanitize()
        {
            Assert.AreEqual(hs.Sanitize("<p>& is an ampersand</p>"), "&lt;p&gt;&amp; is an ampersand&lt;&#x2Fp&gt;");
        }
    }
}
