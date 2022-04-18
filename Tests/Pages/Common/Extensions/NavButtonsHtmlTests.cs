using System.Collections.Generic;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Facade.Quantities;
using Abc.Pages.Common.Aids;
using Abc.Pages.Common.Consts;
using Abc.Pages.Common.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Extensions {

    [TestClass]
    public class NavButtonsHtmlTests : TestsBase {

        private int totalPages;
        private int pageIndex;
        private Args a;
        private List<object> l;
        private string actual;
        private string title;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(NavButtonsHtml);
            title = rndStr;
            totalPages = GetRandom.UInt8(5);
            pageIndex = GetRandom.Int32(1, totalPages - 3);
            a = GetRandom.ObjectOf<Args>();
            a.Title = rndStr;
            a.Handler = rndStr;
            a.Action = rndStr;
            actual = null;
            l = new List<object>();
        }

        [TestCleanup]
        public override void TestCleanup() {
            base.TestCleanup();
            if (actual is null) return;
            Assert.IsTrue(actual.Contains(Href.addPage(a?.PageUrl)));
            Assert.IsTrue(actual.Contains(Href.addAction(a?.Action)));
            Assert.IsTrue(actual.Contains(Href.addHandler(a?.Handler)));
            Assert.IsTrue(actual.Contains(Href.addItemId(a?.ItemId)));
            Assert.IsTrue(actual.Contains(Href.addFixedFilter(a?.FixedFilter)));
            Assert.IsTrue(actual.Contains(Href.addFixedValue(a?.FixedValue)));
            Assert.IsTrue(actual.Contains(Href.addSearchString(a?.SearchString)));
            Assert.IsTrue(actual.Contains(Href.addSortOrder(a?.SortOrder)));
            Assert.IsTrue(actual.Contains(Href.addPageIndex(a?.PageIndex)));
            Assert.IsTrue(actual.Contains(Href.addClass(a?.Disabled)));
            Assert.IsTrue(actual.Contains(Href.addTitle(a?.Title)));
            actual = null;
        }

        [TestMethod]
        public void NavButtonsTest() {
            var obj = new mockHtmlHelper<UnitView>().NavButtons(a, totalPages);
            Assert.IsInstanceOfType(obj, typeof(HtmlContentBuilder));
        }

        [TestMethod]
        public void HtmlStringsTest() {
            var expected = new List<string> {
                "<a href=\"", "&nbsp;", "<a href=\"", "&nbsp;", "Page ", "&nbsp;", "<a href=\"", "&nbsp;", "<a href=\""
            };
            var result = NavButtonsHtml.htmlStrings(a, totalPages);
            htmlContains(result, expected);
        }

        [TestMethod]
        public void AddLastTest() {
            NavButtonsHtml.addLast(l, a, pageIndex, totalPages);
            actual = l[0].ToString();
            Assert.AreEqual("Last", a.Title);
            Assert.AreEqual("Index", a.Handler);
            Assert.AreEqual(null, a.ItemId);
            Assert.AreEqual(totalPages, a.PageIndex);
        }

        [TestMethod]
        public void HasNextTest() {
            static void test(int? x, int? y, string s = "") {
                Assert.AreEqual(s, NavButtonsHtml.hasNext(x, y));
            }

            test(null, null);
            test(totalPages, null);
            test(null, totalPages);
            test(totalPages, totalPages, "disabled");
            test(totalPages - 1, totalPages);
            test(totalPages + 1, totalPages, "disabled");
        }

        [TestMethod]
        public void AddNextTest() {
            NavButtonsHtml.addNext(l, a, pageIndex, totalPages);
            actual = l[0].ToString();
            Assert.AreEqual("Next", a.Title);
            Assert.AreEqual("Index", a.Handler);
            Assert.AreEqual(null, a.ItemId);
            Assert.AreEqual(pageIndex + 1, a.PageIndex);
        }

        [TestMethod]
        public void AddPreviousTest() {
            NavButtonsHtml.addPrevious(l, a, pageIndex);
            actual = l[0].ToString();
            Assert.AreEqual("Previous", a.Title);
            Assert.AreEqual("Index", a.Handler);
            Assert.AreEqual(null, a.ItemId);
            Assert.AreEqual(pageIndex - 1, a.PageIndex);
        }

        [TestMethod]
        public void HasPreviousTest() {
            static void test(int? x, string s = "") => Assert.AreEqual(s, NavButtonsHtml.hasPrevious(x));

            test(null);
            test(totalPages);
            test(1, "disabled");
            test(0, "disabled");
            test(-1, "disabled");
        }

        [TestMethod]
        public void AddFirstTest() {
            NavButtonsHtml.addFirst(l, a, pageIndex);
            actual = l[0].ToString();
            Assert.AreEqual("First", a.Title);
            Assert.AreEqual("Index", a.Handler);
            Assert.AreEqual(null, a.ItemId);
            Assert.AreEqual(1, a.PageIndex);
        }

        [TestMethod]
        public void AddPagesInfoTest() {
            NavButtonsHtml.addPagesInfo(l, pageIndex, totalPages);
            var s = $"<a id=\"pagesInfo\">{Messages.PagesOf.Format(pageIndex, totalPages)}</a>";
            Assert.AreEqual(s, l[0].ToString());
        }

        [TestMethod]
        public void AddSeparatorTest() {
            NavButtonsHtml.addSeparator(l);
            Assert.AreEqual("&nbsp;", l[0].ToString());
        }

        [TestMethod]
        public void HtmlButtonTest() {
            var s = rndStr;
            actual = NavButtonsHtml.htmlButton(a, title, s).ToString();
            Assert.AreEqual(title, a.Title);
            Assert.AreEqual("Index", a.Handler);
            Assert.AreEqual(null, a.ItemId);
            Assert.AreEqual(s, a.Disabled);
        }

    }

}
