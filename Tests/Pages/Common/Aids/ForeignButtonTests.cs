using Abc.Aids.Random;
using Abc.Pages.Common.Aids;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Pages.Common.Aids {
    [TestClass]
    public class ForeignButtonTests :SealedTests<ForeignButton, object> {

        private Uri uri;
        private string caption;
        private string action;
        private string fixedFilter;

        protected override ForeignButton createObject() {
            caption = rndStr;
            action = rndStr;
            uri = GetRandom.ObjectOf<Uri>();
            fixedFilter = rndStr;
            return new ForeignButton(caption, action, uri, fixedFilter);
        }

        [TestMethod] public void ActionTest() => Assert.AreEqual(action, obj.Action);
        [TestMethod] public void CaptionTest() => Assert.AreEqual(caption, obj.Caption);
        [TestMethod] public void ForeignUrlTest() => Assert.AreEqual(uri, obj.ForeignUrl);
        [TestMethod] public void FixedFilterTest() => Assert.AreEqual(fixedFilter, obj.FixedFilter);

        [TestMethod]
        public void GetUrlStringTest() {
            var a = GetRandom.ObjectOf<Args>();
            var idHolder = a.ItemId;
            var actual = obj.GetUrlString(a);
            a.ItemId = idHolder;
            var expected = Href.ComposeForeign(a, uri, action, caption, fixedFilter);
            Assert.AreEqual(expected, actual);
        }
    }
}
