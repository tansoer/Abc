using System;
using Abc.Aids.Random;
using Abc.Pages.Common.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Aids {

    [TestClass]
    public class LinkTests : TestsBase {


        [TestInitialize] public virtual void TestInitialize() => type = typeof(Link);

        [TestMethod]
        public void DisplayNameTest() {
            var n = rndStr;
            var o = new Link(n, (Uri) null);
            Assert.AreEqual(n, o.DisplayName);
            Assert.IsNull(o.Url);
            Assert.AreEqual(n, o.PropertyName);
        }

        [TestMethod]
        public void UrlTest() {
            var n = rndStr;
            var o = new Link(null, new Uri(n, UriKind.Relative));
            Assert.AreEqual(n, o.Url.ToString());
            Assert.IsNull(o.DisplayName);
            Assert.IsNull(o.PropertyName);
        }

        [TestMethod]
        public void PropertyNameTest() {
            var n = rndStr;
            var o = new Link(null, null, n);
            Assert.AreEqual(n, o.PropertyName);
            Assert.IsNull(o.Url);
            Assert.IsNull(o.DisplayName);
        }

    }

}