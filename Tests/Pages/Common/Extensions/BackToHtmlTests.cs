using System;
using Abc.Aids.Random;
using Abc.Facade.Quantities;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {

    [TestClass]
    public class BackToHtmlTests : TestsBase {

        private string str;
        private Args a;

        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(BackToHtml);
            str = rndStr;
            a = GetRandom.ObjectOf<Args>();
            a.Title = rndStr;
            a.Handler = rndStr;
            a.ControlId = rndStr;
        }

        [TestMethod]
        public void BackToTest() {
            var obj = new mockHtmlHelper<UnitView>().BackTo(a);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void HtmlStringsTest() {
            var actual = BackToHtml.htmlStrings(a);
            var s = actual[0].ToString();
            Assert.IsNotNull(s);
            Assert.AreEqual("backToList", a.ControlId);
            var x = $"<a id=\"{a.ControlId}\" href=\"{a.PageUrl}";
            Assert.IsTrue(s.Contains(x));
            Assert.IsTrue(s.Contains($"?handler={a.Handler}"));
            Assert.IsTrue(s.Contains($"&fixedValue={a.FixedValue}"));
            Assert.IsTrue(s.Contains($"&fixedFilter={a.FixedFilter}"));
            Assert.IsTrue(s.Contains($"&sortOrder={a.SortOrder}"));
            Assert.IsTrue(s.Contains($"&searchString={a.SearchString}"));
            Assert.IsTrue(s.Contains($"&pageIndex={a.PageIndex}"));
            Assert.IsTrue(s.Contains($"\">{a.Title}</a>"));
        }

        [TestMethod]
        public void GetTitleTest() {
            Assert.AreEqual(Captions.BackToList, BackToHtml.getTitle(null));
            Assert.AreEqual(str, BackToHtml.getTitle(str));
        }

        [TestMethod]
        public void GetHandlerTest() {
            Assert.AreEqual(Actions.Index, BackToHtml.getHandler(null));
            Assert.AreEqual(str, BackToHtml.getHandler(str));
        }

        [TestMethod]
        public void GetPageUrlTest() {
            Assert.AreEqual(Actions.Index, BackToHtml.getPageUrl(null).ToString());
            Assert.AreEqual(str, BackToHtml.getPageUrl(new Uri(str, UriKind.Relative)).ToString());
        }

    }

}
