using System;
using Abc.Aids.Random;
using Abc.Pages.Common;
using Abc.Pages.Common.Consts;
using Abc.Tests.Data;
using Abc.Tests.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common {

    [TestClass] public class BasePageTests : 
        AbstractPageTests<BasePage<ITestRepo,TestObject, TestData>, BasePageAids> {
        private string s;
        private int i;

        protected override BasePage<ITestRepo, TestObject,  TestData> createObject() =>  new testClass(db);
        
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            s = rndStr;
            i = GetRandom.Int16();
            obj.FixedValue = s;
            obj.FixedFilter = s;
            obj.SortOrder = s;
            obj.CurrentFilter = s;
            obj.SearchString = s;
            obj.SortOrder = s;
        }
        private static void test<T>(T s, Action<T> set, Func<T> get, Func<T> getDb) {
            set(s);
            Assert.AreEqual(s, get());
            Assert.AreEqual(s, getDb());
        }
        [TestMethod] public void FixedValueTest()
            => test(rndStr, x => obj.FixedValue = x, () => obj.FixedValue, () => db.FixedValue);

        [TestMethod]
        public void FixedValuesTest()
            => test(GetRandom.ArrayOf(typeof(string)), x => obj.FixedValues = (string[])x, () => obj.FixedValues,
                () => db.FixedValues);

        [TestMethod] public void HasFixedFilterTest() {
            obj.FixedFilter = null;
            Assert.IsFalse(obj.HasFixedFilter);
            obj.FixedFilter = rndStr;
            Assert.IsTrue(obj.HasFixedFilter);
        }
        [TestMethod] public void FixedFilterTest() 
            => test(rndStr, x => obj.FixedFilter = x, () => obj.FixedFilter, () => db.FixedFilter);

        [TestMethod] public void SortOrderTest() 
            => test(rndStr, x => obj.SortOrder = x, () => obj.SortOrder, () => db.SortOrder);

        [TestMethod] public void CurrentFilterTest()
            => test(rndStr, x => obj.CurrentFilter = x, () => obj.CurrentFilter, () => db.CurrentFilter);

        [TestMethod] public void GetSortOrderTest() {
            void test(string sortOrder, string name, bool isDesc) {
                obj.SortOrder = sortOrder;
                var actual = obj.getSortOrder(name);
                var expected = isDesc ? name + "_desc" : name;
                Assert.AreEqual(expected, actual);
            }
            test(null, rndStr, false);
            test(rndStr, rndStr, false);
            var s = rndStr;
            test(s, s, true);
            test(s + "_desc", s, false);
        }
        [TestMethod] public void SearchStringTest()
            => test(rndStr, x => obj.SearchString = x, () => obj.SearchString, () => db.SearchString);

        [TestMethod] public void SortUrlTest() {
            obj = new testClass(db, new Uri("xxx/yyy", UriKind.Relative)) {
                SortOrder = "Code",
                SearchString = "AAA",
                FixedFilter = "BBB",
                FixedValue = "CCC",
                CurrentFilter = "DDD"
            };
            var sortString = obj.SortUrl(x => x.Code);
            var s = "xxx/yyy/Index?handler=Index&sortOrder=Code_desc&searchString=AAA&currentFilter=DDD&fixedFilter=BBB&fixedValue=CCC";
            Assert.AreEqual(s, sortString.ToString());
        }
        [TestMethod] public void PageIndexTest()
            => test(GetRandom.Int32(10, 100), x => obj.PageIndex = x, () => obj.PageIndex, () => db.PageIndex);

        [TestMethod] public void PageUrlTest() => isReadOnly(new Uri(obj.pageUrl, UriKind.Relative));
        [TestMethod] public void IndexUrlTest() => isReadOnly(obj.indexUrl());
        [TestMethod] public void CreateUrlTest() => isReadOnly(obj.createUrl());
        [TestMethod] public void HandlerUrlTest()  => Assert.AreEqual(obj.handlerUrl(s), $"/Test/{s}?handler={s}");
        [TestMethod] public void PageIndexUrlTest() => Assert.AreEqual(obj.pageIndexUrl(i), $"&pageIndex={i}");
        [TestMethod] public void SortOrderUrlTest() 
            => Assert.AreEqual(obj.sortOrderUrl(), $"&sortOrder={obj.SortOrder}");
        [TestMethod] public void SearchStringUrlTest() 
            => Assert.AreEqual(obj.searchStringUrl(), $"&searchString={obj.SearchString}");
        [TestMethod] public void FixedFilterUrlTest()
            => Assert.AreEqual(obj.fixedFilterUrl(), $"&fixedFilter={obj.FixedFilter}");
        [TestMethod] public void FixedValueUrlTest()
            => Assert.AreEqual(obj.fixedValueUrl(), $"&fixedValue={obj.FixedValue}");
        [TestMethod] public void CurrentFilterUrlTest()
            => Assert.AreEqual(obj.currentFilterUrl(), $"&currentFilter={obj.CurrentFilter}");
        [TestMethod] public void SwitchUrlTest()
            => Assert.AreEqual(obj.switchUrl(i), $"&switchOfCreate={i}");
        [TestMethod] public void PageIdxTest() => Assert.AreEqual(obj.pageIndex(i), i);

        [TestMethod] public void IndexHandlerUrlTest() 
            => Assert.AreEqual(obj.indexHandlerUrl, obj.handlerUrl(Actions.Index));
        [TestMethod] public void CreateHandlerUrlTest() 
            => Assert.AreEqual(obj.createHandlerUrl, obj.handlerUrl(Actions.Create));
        [TestMethod] public void EditHandlerUrlTest() 
            => Assert.AreEqual(obj.editHandlerUrl, obj.handlerUrl(Actions.Edit));
        [TestMethod] public void DetailsHandlerUrlTest() 
            => Assert.AreEqual(obj.detailsHandlerUrl, obj.handlerUrl(Actions.Details));
        [TestMethod] public void DeleteHandlerUrlTest() 
            => Assert.AreEqual(obj.deleteHandlerUrl, obj.handlerUrl(Actions.Delete));
        [TestMethod] public void SelectHandlerUrlTest()
            => Assert.AreEqual(obj.selectHandlerUrl(), obj.handlerUrl(Actions.Select));
        [TestMethod] public void AttributesTest()
            => Assert.AreEqual(obj.attributes, obj.pageIndexUrl() + obj.sortOrderUrl() + obj.filters);
        [TestMethod] public void FiltersTest()
            => Assert.AreEqual(obj.filters, obj.searchStringUrl() + obj.currentFilterUrl() + obj.fixedFilters);
        [TestMethod] public void FixedFiltedsTest()
            => Assert.AreEqual(obj.fixedFilters, obj.fixedFilterUrl() + obj.fixedValueUrl());
        [TestMethod] public void CreateUriTest()
            => Assert.AreEqual(obj.createUrl().ToString(), obj.createHandlerUrl + obj.attributes);
        [TestMethod] public void SwitchCreateUriTest()
            => Assert.AreEqual(obj.createUri(i).ToString(), obj.createUrl() + obj.switchUrl(i));
        [TestMethod] public void IndexUriTest()
            => Assert.AreEqual(obj.indexUrl().ToString(), obj.indexHandlerUrl + obj.fixedFilters);
        [TestMethod] public void SortIndexUriTest()
            => Assert.AreEqual(obj.indexUri(s), obj.indexHandlerUrl + obj.sortOrderUrl(s) + obj.filters);
    }
}
