using System;
using Abc.Aids.Random;
using Abc.Pages.Common.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common.Aids {

    [TestClass]
    public class ArgsTests : SealedTests<Args, object> {

        private Uri pageUrl;
        private string itemId;
        private string fixedFilter;
        private string fixedValue;
        private string sortOrder;
        private string searchString;
        private string currentFilter;
        private int? pageIndex;
        private string handler;
        private string title;
        private string action;
        private string disabled;
        private string controlId;

        protected override Args createObject() {
            pageIndex = GetRandom.UInt8();
            pageUrl = new Uri(rndStr, UriKind.Relative);
            itemId = rndStr;
            fixedFilter = rndStr;
            fixedValue = rndStr;
            sortOrder = rndStr;
            searchString = rndStr;
            currentFilter = rndStr;
            action = rndStr;
            disabled = rndStr;
            controlId = rndStr;
            var o = new Args(pageUrl, itemId, fixedFilter, fixedValue, sortOrder, searchString, pageIndex, currentFilter);
            handler = rndStr;
            title = rndStr;
            o.Handler = handler;
            o.Title = title;
            o.Action = action;
            o.Disabled = disabled;
            o.ControlId = controlId;
            return o;
        }

        [TestMethod] public void PageUrlTest() => Assert.AreEqual(pageUrl, obj.PageUrl);

        [TestMethod] public void PageIndexTest() => Assert.AreEqual(pageIndex, obj.PageIndex);

        [TestMethod] public void ItemIdTest() => Assert.AreEqual(itemId, obj.ItemId);

        [TestMethod] public void FixedFilterTest() => Assert.AreEqual(fixedFilter, obj.FixedFilter);

        [TestMethod] public void FixedValueTest() => Assert.AreEqual(fixedValue, obj.FixedValue);

        [TestMethod] public void SortOrderTest() => Assert.AreEqual(sortOrder, obj.SortOrder);

        [TestMethod] public void SearchStringTest() => Assert.AreEqual(searchString, obj.SearchString);
        [TestMethod] public void CurrentFilterTest() => Assert.AreEqual(currentFilter, obj.CurrentFilter);

        [TestMethod] public void HandlerTest() => Assert.AreEqual(handler, obj.Handler);

        [TestMethod] public void TitleTest() => Assert.AreEqual(title, obj.Title);
        [TestMethod] public void ActionTest() => Assert.AreEqual(action, obj.Action);
        [TestMethod] public void DisabledTest() => Assert.AreEqual(disabled, obj.Disabled);
        [TestMethod] public void ControlIdTest() => Assert.AreEqual(controlId, obj.ControlId);

        [TestMethod]
        public void RowArgsTest() {
            Assert.IsNotNull(new Args());
        }

        [TestMethod]
        public void CreateRowArgsTest() {
            var a = new Args(pageUrl, itemId, fixedFilter,
                fixedValue, sortOrder, searchString, pageIndex);
            Assert.AreEqual(pageUrl, a.PageUrl);
            Assert.AreEqual(itemId, a.ItemId);
            Assert.AreEqual(fixedFilter, a.FixedFilter);
            Assert.AreEqual(fixedValue, a.FixedValue);
            Assert.AreEqual(sortOrder, a.SortOrder);
            Assert.AreEqual(searchString, a.SearchString);
            Assert.AreEqual(pageIndex, a.PageIndex);
            Assert.AreEqual(null, a.Handler);
            Assert.AreEqual(null, a.Title);
        }

    }

}
