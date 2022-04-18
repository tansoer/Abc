using System;
using Abc.Aids.Random;
using Abc.Pages.Common.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Aids {

    [TestClass]
    public class HrefTests : TestsBase {

        private string s;
        private int i;

        [TestInitialize]
        public virtual void TestInitialize() {
            type = typeof(Href);
            s = rndStr;
            i = GetRandom.UInt8(5);
        }

        [TestMethod]
        public void ComposeForeignTest() {
            var a = GetRandom.ObjectOf<Args>();
            var uri = GetRandom.ObjectOf<Uri>();
            var handler = rndStr;
            var title = rndStr;
            var fixedFilter = rndStr;
            var fixedValue = a.ItemId;
            var actual = Href.ComposeForeign(a, uri, handler, title, fixedFilter);
            Assert.IsTrue(actual.Contains(Href.addPage(uri)));
            Assert.IsTrue(actual.Contains(Href.addAction(handler)));
            Assert.IsTrue(actual.Contains(Href.addHandler(handler)));
            Assert.IsNull(a.ItemId);
            Assert.IsTrue(actual.Contains(Href.addFixedFilter(fixedFilter)));
            Assert.IsTrue(actual.Contains(Href.addFixedValue(fixedValue)));
            Assert.IsTrue(actual.Contains(Href.addSearchString(a?.SearchString)));
            Assert.IsTrue(actual.Contains(Href.addSortOrder(a?.SortOrder)));
            Assert.IsTrue(actual.Contains(Href.addPageIndex(a?.PageIndex)));
            Assert.IsTrue(actual.Contains(Href.addTitle(title)));
        }

        [TestMethod]
        public void HandlerTest() {
            var a = GetRandom.ObjectOf<Args>();
            var handler = rndStr;
            var title = rndStr;
            var actual = Href.Compose(a, handler, title);
            Assert.IsTrue(actual.Contains(Href.addPage(a?.PageUrl)));
            Assert.IsTrue(actual.Contains(Href.addAction(handler)));
            Assert.IsTrue(actual.Contains(Href.addHandler(handler)));
            Assert.IsTrue(actual.Contains(Href.addItemId(a?.ItemId)));
            Assert.IsTrue(actual.Contains(Href.addFixedFilter(a?.FixedFilter)));
            Assert.IsTrue(actual.Contains(Href.addFixedValue(a?.FixedValue)));
            Assert.IsTrue(actual.Contains(Href.addSearchString(a?.SearchString)));
            Assert.IsTrue(actual.Contains(Href.addSortOrder(a?.SortOrder)));
            Assert.IsTrue(actual.Contains(Href.addPageIndex(a?.PageIndex)));
            Assert.IsTrue(actual.Contains(Href.addTitle(title)));
        }

        [TestMethod]
        public void ComposeTest() {
            Assert.AreEqual("<a href=\"#\"></a>", Href.Compose(null));
            var a = new Args();
            Assert.AreEqual("<a href=\"#\"></a>", Href.Compose(a));
            a = GetRandom.ObjectOf<Args>();
            var actual = Href.Compose(a);
            Assert.IsTrue(actual.Contains(Href.addPage(a?.PageUrl)));
            Assert.IsTrue(actual.Contains(Href.addAction(a?.Action)));
            Assert.IsTrue(actual.Contains(Href.addHandler(a?.Handler)));
            Assert.IsTrue(actual.Contains(Href.addItemId(a?.ItemId)));
            Assert.IsTrue(actual.Contains(Href.addFixedFilter(a?.FixedFilter)));
            Assert.IsTrue(actual.Contains(Href.addFixedValue(a?.FixedValue)));
            Assert.IsTrue(actual.Contains(Href.addSearchString(a?.SearchString)));
            Assert.IsTrue(actual.Contains(Href.addSortOrder(a?.SortOrder)));
            Assert.IsTrue(actual.Contains(Href.addPageIndex(a?.PageIndex)));
            Assert.IsTrue(actual.Contains(Href.addTitle(a?.Title)));
        }

        [TestMethod]
        public void AddPageTest() {
            var actual = Href.addPage(new Uri(s, UriKind.Relative));
            Assert.AreEqual($"<a href=\"{s}", actual);
            Assert.AreEqual("<a href=\"#", Href.addPage(null));
        }

        [TestMethod]
        public void AddActionTest() {
            var actual = Href.addAction(s);
            Assert.AreEqual($"/{s}?", actual);
            Assert.AreEqual("?", Href.addAction(null));
        }

        [TestMethod]
        public void AddHandlerTest() {
            var actual = Href.addHandler(s);
            Assert.AreEqual($"handler={s}", actual);
            Assert.AreEqual(string.Empty, Href.addHandler(null));
        }

        [TestMethod]
        public void AddItemIdTest() {
            var actual = Href.addItemId(s);
            Assert.AreEqual($"&id={s}", actual);
            Assert.AreEqual(string.Empty, Href.addItemId(null));
        }

        [TestMethod]
        public void AddFixedFilterTest() {
            var actual = Href.addFixedFilter(s);
            Assert.AreEqual($"&fixedFilter={s}", actual);
            Assert.AreEqual(string.Empty, Href.addFixedFilter(null));
        }

        [TestMethod]
        public void AddFixedValueTest() {
            var actual = Href.addFixedValue(s);
            Assert.AreEqual($"&fixedValue={s}", actual);
            Assert.AreEqual(string.Empty, Href.addFixedValue(null));
        }

        [TestMethod]
        public void AddSortOrderTest() {
            var actual = Href.addSortOrder(s);
            Assert.AreEqual($"&sortOrder={s}", actual);
            Assert.AreEqual(string.Empty, Href.addSortOrder(null));
        }

        [TestMethod]
        public void AddSearchStringTest() {
            var actual = Href.addSearchString(s);
            Assert.AreEqual($"&searchString={s}", actual);
            Assert.AreEqual(string.Empty, Href.addSearchString(null));
        }

        [TestMethod]
        public void AddPageIndexTest() {
            var actual = Href.addPageIndex(i);
            Assert.AreEqual($"&pageIndex={i}", actual);
            Assert.AreEqual(string.Empty, Href.addPageIndex(null));
        }

        [TestMethod]
        public void AddTitleTest() {
            var actual = Href.addTitle(s);
            Assert.AreEqual($"\">{s}</a>", actual);
            Assert.AreEqual("\"></a>", Href.addTitle(null));
        }

        [TestMethod]
        public void RemoveTest() {
            Assert.AreEqual(null, Href.remove(null));
            Assert.AreEqual(string.Empty, Href.remove(string.Empty));
            Assert.AreEqual(string.Empty, Href.remove(string.Empty));
            Assert.AreEqual("", Href.remove("?"));
            Assert.AreEqual("?", Href.remove("??"));
            Assert.AreEqual(s, Href.remove(s));
            Assert.AreEqual(s, Href.remove(s + "?"));
        }

    }

}
