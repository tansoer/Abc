using Abc.Aids.Random;
using Abc.Pages.Common;
using Abc.Tests.Data;
using Abc.Tests.Domain;
using Abc.Tests.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common {

    [TestClass]
    public class CrudPageTests : AbstractPageTests<CrudPage<ITestRepo, TestObject, TestView, TestData>,
        CommonPage<ITestRepo, TestObject, TestView, TestData>> {

        protected override CrudPage<
            ITestRepo, 
            TestObject, 
            TestView, 
            TestData> createObject() {
            var o = new testClass(db);
            Assert.AreEqual(null, o.FixedValue);
            Assert.AreEqual(null, o.FixedFilter);
            return o;
        }
        [TestMethod] public void ItemTest() => isProperty(() => obj.Item, x => obj.Item = x);
        [TestMethod] public void AddTest() {
            var idx = db.list.Count;
            obj.Item = GetRandom.ObjectOf<TestView>();
            obj.add().GetAwaiter();
            allPropertiesAreEqual(obj.Item, db.list[idx].Data);
        }
        [TestMethod] public void UpdateTest() {
            GetTest();
            var idx = GetRandom.Int32(0, db.list.Count);
            var itemId = db.list[idx].Data.Id;
            obj.Item = GetRandom.ObjectOf<TestView>();
            obj.Item.Id = itemId;
            obj.update().GetAwaiter();
            allPropertiesAreEqual(db.list[^1].Data, obj.Item);
        }
        [TestMethod] public void GetTest() {
            var count = GetRandom.UInt8(5, 10);
            var idx = GetRandom.UInt8(0, count);
            for (var i = 0; i < count; i++) AddTest();
            var item = db.list[idx];
            obj.get(item.Data.Id).GetAwaiter();
            Assert.AreEqual(count, db.list.Count);
            allPropertiesAreEqual(item.Data, obj.Item);
        }
        [TestMethod] public void DeleteTest() {
            AddTest();
            obj.delete(obj.Item.Id).GetAwaiter();
            Assert.AreEqual(0, db.list.Count);
        }
        [TestMethod] public void ToViewTest() {
            var d = GetRandom.ObjectOf<TestData>();
            var v = obj.toView(new TestObject(d));
            allPropertiesAreEqual(d, v);
        }
        [TestMethod] public void ToObjectTest() {
            var v = GetRandom.ObjectOf<TestView>();
            var o = obj.toObject(v);
            allPropertiesAreEqual(v, o.Data);
        }
        [TestMethod] public void ItemIdTest() {
            obj.Item = GetRandom.ObjectOf<TestView>();
            Assert.AreEqual(obj.Item.Id, obj.ItemId);
        }
    }
}
