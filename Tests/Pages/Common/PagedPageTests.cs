using Abc.Aids.Random;
using Abc.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abc.Tests.Data;
using Abc.Tests.Domain;
using Abc.Tests.Facade;
using System.Collections.Generic;

namespace Abc.Tests.Pages.Common {

    [TestClass]  public class PagedPageTests : AbstractPageTests<
        PagedPage<TestPage, ITestRepo, TestObject, TestView, TestData>,
        ItemsPage<TestPage, ITestRepo, TestObject, TestView, TestData>> {
        protected override PagedPage<TestPage, ITestRepo, TestObject, TestView, TestData> createObject() => new testClass(db);
        [TestMethod] public void ItemsTest()  => isProperty<IList<TestView>>();
        [TestMethod] public void HasPreviousPageTest() {
            db.HasPreviousPage = rndBool;
            isReadOnly(db.HasPreviousPage);
        }
        [TestMethod] public void HasNextPageTest() {
            db.HasNextPage = rndBool;
            isReadOnly(db.HasNextPage);
        }
        [TestMethod] public void TotalPagesTest() {
            db.TotalPages = GetRandom.UInt8();
            isReadOnly(db.TotalPages);
        }
        [TestMethod] public void GetListTest() {
            Assert.IsNull(obj.Items);
            var sortOrder = rndStr;
            var currentFilter = rndStr;
            var searchString = rndStr;
            var fixedFilter = rndStr;
            var fixedValue = rndStr;
            var pageIndex = GetRandom.UInt8();
            obj.getList(sortOrder, currentFilter, searchString, pageIndex, fixedFilter, fixedValue, null).GetAwaiter();
            Assert.IsNotNull(obj.Items);
            Assert.AreEqual(sortOrder, obj.SortOrder);
            Assert.AreEqual(searchString, obj.SearchString);
            Assert.AreEqual(fixedFilter, obj.FixedFilter);
            Assert.AreEqual(fixedValue, obj.FixedValue);
            Assert.AreEqual(1, obj.PageIndex);
        }
        [TestMethod] public void GetListNoArgumentsTest() {
            var l = obj.getList().GetAwaiter().GetResult();
            Assert.AreEqual(0, l.Count);
            for (var i = 0; i < GetRandom.UInt8(); i++) {
                var d = GetRandom.ObjectOf<TestData>();
                db.Add(new TestObject(d));
                l = obj.getList().GetAwaiter().GetResult();
                Assert.AreEqual(i + 1, l.Count);
            }
        }
        [TestMethod] public void SelectedIdTest() => isNullable<string>();
        [TestMethod] public void GetPageIndexTest() {
            static void test(string filter, string searchString, int? pageIndex, bool isFirst) {
                var expectedIndex = isFirst ? 1 : pageIndex;
                var actual = PagedPage<TestPage, ITestRepo, TestObject, TestView, TestData>.getPageIndex(filter, searchString, pageIndex);
                Assert.AreEqual(expectedIndex, actual);
            }
            test(rndStr, rndStr, GetRandom.UInt8(3), true);
            test(rndStr, null, GetRandom.UInt8(3), true);
            var s = rndStr;
            test(s, s, GetRandom.UInt8(3), false);
        }
    }
}
