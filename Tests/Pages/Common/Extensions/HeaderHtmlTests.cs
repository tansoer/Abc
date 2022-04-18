using System.Collections.Generic;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Facade.Quantities;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {

    [TestClass]
    public class HeaderHtmlTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(HeaderHtml);

        [TestMethod]
        public void HeaderTest() {
            var obj = new mockHtmlHelper<UnitView>().Header(
                rndStr,
                rndStr,
                rndStr,
                rndStr);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void LinkHeaderTest() {
            var obj = new mockHtmlHelper<UnitView>().Header(
                GetRandom.ObjectOf<Link>(),
                GetRandom.ObjectOf<Link>(),
                GetRandom.ObjectOf<Link>(),
                GetRandom.ObjectOf<Link>()
            );
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void AddHeaderTest() {
            var l = new List<object>();
            var s = rndStr;
            HeaderHtml.addHeader(l, s);
            Assert.AreEqual(3, l.Count);
            Assert.AreEqual("<th>", l[0].ToString());
            Assert.AreEqual(s, l[1].ToString());
            Assert.AreEqual("</th>", l[2].ToString());
        }

        [TestMethod]
        public void AddLinkTest() {
            var l = new List<object>();
            var link = GetRandom.ObjectOf<Link>();
            var id = link.PropertyName.ToLowerCase().RemoveSpaces() + "Column";
            HeaderHtml.addLink(l, link);
            Assert.AreEqual(3, l.Count);
            Assert.AreEqual("<th>", l[0].ToString());
            Assert.AreEqual($"<a id=\"{id}\" href=\"{link.Url}\"><span style=\"font-weight:normal\">{link.DisplayName}</span></a>",
                l[1].ToString());
            Assert.AreEqual("</th>", l[2].ToString());
        }

        [TestMethod]
        public void NameStringsTest() {
            var s = rndStr;
            var l = HeaderHtml.htmlStrings(s);
            Assert.AreEqual(6, l.Count);
            Assert.AreEqual("<tr>", l[0].ToString());
            Assert.AreEqual("<th>", l[1].ToString());
            Assert.AreEqual(s, l[2].ToString());
            Assert.AreEqual("</th>", l[3].ToString());
            Assert.AreEqual("<th></th>", l[4].ToString());
            Assert.AreEqual("</tr>", l[5].ToString());
        }

        [TestMethod]
        public void LinkStringTest() {
            var link = GetRandom.ObjectOf<Link>();
            var l = HeaderHtml.htmlStrings(link);
            var id = link.PropertyName.ToLowerCase().RemoveSpaces() + "Column";
            Assert.AreEqual(6, l.Count);
            Assert.AreEqual("<tr>", l[0].ToString());
            Assert.AreEqual("<th>", l[1].ToString());
            Assert.AreEqual($"<a id=\"{id}\" href=\"{link.Url}\"><span style=\"font-weight:normal\">{link.DisplayName}</span></a>", l[2].ToString());
            Assert.AreEqual("</th>", l[3].ToString());
            Assert.AreEqual("<th></th>", l[4].ToString());
            Assert.AreEqual("</tr>", l[5].ToString());
        }

    }

}