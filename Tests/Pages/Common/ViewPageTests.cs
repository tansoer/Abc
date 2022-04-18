using Abc.Aids.Random;
using Abc.Pages.Common;
using Abc.Tests.Aids;
using Abc.Tests.Data;
using Abc.Tests.Domain;
using Abc.Tests.Facade;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common {
    [TestClass] public class ViewPageTests : AbstractPageTests<
    ViewPage<TestPage, ITestRepo, TestObject, TestView, TestData>,
    TitledPage<TestPage, ITestRepo, TestObject, TestView, TestData>> {
        private testRepo Repo;
        private int count;
        private string sortOrder;
        private string id;
        private string currentFilter;
        private string searchString;
        private byte pageIndex;
        private string fixedFilter;
        private string fixedValue;
        private byte createSwitch;
        protected override ViewPage<TestPage, ITestRepo, TestObject, TestView, TestData> createObject() {
            Repo = new testRepo();
            setRandomValues();
            addRandomItems();
            return new testClass(Repo);
        }
        private void setRandomValues() {
            sortOrder = rndStr;
            id = rndStr;
            currentFilter = rndStr;
            searchString = rndStr;
            pageIndex = GetRandom.UInt8();
            fixedFilter = rndStr;
            fixedValue = rndStr;
            createSwitch = GetRandom.UInt8(0, 10);
        }
        private void addRandomItems() {
            count = GetRandom.UInt8(10, 100);
            for (var i = 0; i < count; i++)
                Repo.Add(createTestObject());
        }
        private static TestObject createTestObject() {
            var d = GetRandom.ObjectOf<TestData>();
            d.Type = GetRandom.EnumOf<TestType>();
            return new TestObject(d);
        }
        [TestMethod] public void OnGetIndexAsyncTest() {
            isNull(obj.Items);
            obj.OnGetIndexAsync(null, null, null, null, null, null, null).GetAwaiter();
            areEqual(count, obj.Items.Count);
        }
        [TestMethod] public void OnGetIndexAsyncArgumentsTest() {
            isNull(obj.Items);
            obj.OnGetIndexAsync(sortOrder, id, currentFilter, searchString, pageIndex, fixedFilter, fixedValue)
                .GetAwaiter();
            areEqual(0, obj.Items.Count);
            testPageProperties(id, 1);
        }
        [TestMethod] public void OnGetCreateTest() {
            var page = obj.OnGetCreate(sortOrder, searchString, pageIndex, fixedFilter, fixedValue, createSwitch);
            isInstanceOfType<PageResult>(page);
            testPageProperties();
        }
        [TestMethod] public void OnPostCreateAsyncTest() {
            obj.Item = GetRandom.ObjectOf<TestView>();
            var o = Repo.Get(obj.Item.Id);
            isNull(o);
            obj.OnPostCreateAsync(sortOrder, searchString, pageIndex, fixedFilter, fixedValue).GetAwaiter();
            o = Repo.Get(obj.Item.Id);
            isNotNull(o);
            isFalse(o.IsUnspecified);
            allPropertiesAreEqual(obj.Item, o.Data);
            testPageProperties();
        }
        [TestMethod] public void OnGetDeleteAsyncTest() {
            obj.OnGetDeleteAsync(id, sortOrder, searchString, pageIndex, fixedFilter, fixedValue).GetAwaiter();
            allPropertiesAreEqual(new TestData(), obj.Item, nameof(obj.Item.Id));
            var idx = GetRandom.Int32(0, count);
            var m = Repo.list[idx];
            obj.OnGetDeleteAsync(m.Data.Id, sortOrder, searchString, pageIndex, fixedFilter, fixedValue).GetAwaiter();
            allPropertiesAreEqual(m.Data, obj.Item);
            testPageProperties();
        }
        private void testPageProperties(string selId = null, int? pageIdx = null) {
            areEqual(selId, obj.SelectedId);
            areEqual(fixedFilter, obj.FixedFilter);
            areEqual(fixedValue, obj.FixedValue);
            areEqual(searchString, obj.SearchString);
            areEqual(pageIdx ?? pageIndex, obj.PageIndex);
            areEqual(sortOrder, obj.SortOrder);
        }
        [TestMethod] public void OnPostDeleteAsyncTest() {
            var idx = GetRandom.Int32(0, count);
            var expected = Repo.list[idx];
            var actual = Repo.Get(expected.Data.Id);
            allPropertiesAreEqual(expected.Data, actual.Data);
            obj.OnPostDeleteAsync(expected.Data.Id, sortOrder, searchString, pageIndex, fixedFilter, fixedValue)
                .GetAwaiter();
            actual = Repo.Get(expected.Data.Id);
            isNull(actual);
            testPageProperties();
        }
        [TestMethod] public void OnGetDetailsAsyncTest() {
            obj.OnGetDetailsAsync(id, sortOrder, searchString, pageIndex, fixedFilter, fixedValue).GetAwaiter();
            allPropertiesAreEqual(new TestView(), obj.Item, nameof(obj.Item.Id));
            var idx = GetRandom.Int32(0, count);
            var m = Repo.list[idx];
            obj.OnGetDeleteAsync(m.Data.Id, sortOrder, searchString, pageIndex, fixedFilter, fixedValue).GetAwaiter();
            allPropertiesAreEqual(m.Data, obj.Item);
            testPageProperties();
        }
        [TestMethod]
        public void OnGetEditAsyncTest() {
            obj.OnGetEditAsync(id, sortOrder, searchString, pageIndex, fixedFilter, fixedValue).GetAwaiter();
            allPropertiesAreEqual(new TestView(), obj.Item, nameof(obj.Item.Id));
            var idx = GetRandom.Int32(0, count);
            var m = Repo.list[idx];
            obj.OnGetEditAsync(m.Data.Id, sortOrder, searchString, pageIndex, fixedFilter, fixedValue).GetAwaiter();
            allPropertiesAreEqual(m.Data, obj.Item);
            testPageProperties();
        }
        [TestMethod] public void OnPostEditAsyncTest() {
            obj.Item = GetRandom.ObjectOf<TestView>();
            var o = Repo.Get(obj.Item.Id);
            isNull(o);
            obj.OnPostEditAsync(sortOrder, searchString, pageIndex, fixedFilter, fixedValue).GetAwaiter();
            o = Repo.Get(obj.Item.Id);
            isNotNull(o);
            isFalse(o.IsUnspecified);
            allPropertiesAreEqual(o.Data, obj.Item);
            testPageProperties();
        }
        [TestMethod] public void ColumnsTest() => isReadOnly(obj.Columns);
        [TestMethod] public void ColumnsCountTest() { 
            areEqual(7, obj.ColumnsCount);
            obj.Columns.Clear();
            areEqual(0, obj.ColumnsCount);
        }
        [TestMethod] public void RowsCountTest() {
            obj.OnGetIndexAsync(null, null, null, null, null, null, null).GetAwaiter();
            areEqual(obj.Items.Count, obj.RowsCount);
            obj.Items.Clear();
            areEqual(0, obj.RowsCount);
        }
        [TestMethod] public void SetItemTest() {
            obj.OnGetIndexAsync(null, null, null, null, null, null, null).GetAwaiter();
            var i = GetRandom.Int32(0, obj.Items.Count - 1);
            obj.SetItem(i);
            allPropertiesAreEqual(obj.Items[i], obj.Item);
        }
        [DataTestMethod]
        [DataRow(0, "Id")]
        [DataRow(1, "Name")]
        [DataRow(2, "Code")]
        [DataRow(3, "Details")]
        [DataRow(100, "Unspecified")]
        public void NameForTest(int i, string param) {
            var mock = new mockHtmlHelper<TestPage>();
            isTrue(obj.NameFor(mock, i).Contains(param));
        }
        [DataTestMethod]
        [DataRow(0, "Id")]
        [DataRow(1, "Name")]
        [DataRow(2, "Code")]
        [DataRow(3, "Details")]
        public void ValueForTest(int i, string param) {
            var mock = new mockHtmlHelper<TestPage>();
            isTrue(obj?.ValueFor(mock, i)?.ToString()?.Contains(param) ?? false);
            isNull(obj?.ValueFor(mock, 100));
        }

        [TestMethod] public void SortStringForTest() {
            var columnsCount = obj.ColumnsCount;
            var i = GetRandom.Int32(columnsCount, 100);
            var s = obj.SortStringFor(i);
            isNull(s, $"For i = {i}, the SortStringFor returns {s}");
        }

        [TestMethod] public void SortUrlTest() {
            var actual = obj.SortUrl(x => x.Name).ToString();
            var expected =
                $"{obj.PageUrl}/Index?handler=Index&sortOrder=Name&searchString={obj.SearchString}&currentFilter={obj.CurrentFilter}"
                + $"&fixedFilter={obj.FixedFilter}&fixedValue={obj.FixedValue}";
            areEqual(expected, actual);
        }
        [TestMethod] public void LoadDetailsTest() { }
        [TestMethod] public void ButtonsCountTest() {
            areEqual(3, obj.ButtonsCount);
            obj.Buttons.Clear();
            areEqual(0, obj.ButtonsCount);
        }
        [DataTestMethod]
        [DataRow(0, "Edit", "Edit")]
        [DataRow(1, "Details", "Details")]
        [DataRow(2, "Delete", "Delete")]
        [DataRow(100, null, null)]
        public void GetButtonTest(int i, string expectedAction, string expectedCaption) {
            var actual = obj.GetButton(i);
            areEqual(actual?.Action, expectedAction);
            areEqual(actual?.Caption, expectedCaption);
        }
        [TestMethod] public void ButtonsTest() => isReadOnly(obj.Buttons);
        [TestMethod] public void EditorsTest() => isReadOnly(obj.Editors);
        [TestMethod] public void ViewersTest() => isReadOnly(obj.Viewers);
        //TODO Is testing like this OK?
        [TestMethod]
        public void EditorForTest() {
            var mock = new mockHtmlHelper<TestPage>();
            var cnt = obj.Editors.Count;
            for (int i = 0; i < cnt; i++) {
                areEqual(obj.EditorFor(mock, i).GetType(), typeof(IHtmlContent));
            }
            isNull(obj.EditorFor(mock, cnt));
        }
        [TestMethod]
        public void ViewerForTest() {
            var mock = new mockHtmlHelper<TestPage>();
            var cnt = obj.Viewers.Count;
            for (int i = 0; i < cnt; i++) {
                areEqual(obj.ViewerFor(mock, i).GetType(), typeof(IHtmlContent));
            }
            isNull(obj.ViewerFor(mock, cnt));
        }
    }
}