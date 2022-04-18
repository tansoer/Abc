using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Pages.Common;
using Abc.Tests.Aids;
using Abc.Tests.Data;
using Abc.Tests.Domain;
using Abc.Tests.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common
{
    [TestClass]
    public class ViewsPageTests :AbstractPageTests<
        ViewsPage<TestPage, ITestRepo, TestObject, TestView, TestData, TestType>,
        ViewPage<TestPage, ITestRepo, TestObject, TestView, TestData>> {

        private testRepo Repo;
        private int count;

        protected override ViewsPage<TestPage, ITestRepo, TestObject, TestView, TestData, TestType> createObject() {
            Repo = new testRepo();
            addRandomItems();
            return new testClass(Repo);
        }
        private void addRandomItems() {
            count = GetRandom.UInt8(10, 100);
            for (var i = 0; i < count; i++)
                Repo.Add(createTestObject());
        }

        private TestObject createTestObject() {
            var d = GetRandom.ObjectOf<TestData>();
            d.Type = GetRandom.EnumOf<TestType>();
            return new TestObject(d);
        }

        [TestMethod] public void DropDownEntryCountTest() 
            => Assert.AreEqual(obj.IndexCreateButtonDropDownEntries.Count, obj.DropDownEntryCount);

        [TestMethod] public void IndexCreateButtonDropDownEntriesTest()
            => isReadOnly(obj.IndexCreateButtonDropDownEntries);

        [DataRow(TestType.TestType1, 1)]
        [DataRow(TestType.TestType2, 2)]
        [DataRow(TestType.TestType3, 3)]
        [DataRow(TestType.TestType4, 4)]
        [TestMethod] public void CreateUriTest(TestType variant, int expected) 
            => Assert.AreEqual(obj.createUri(expected), obj.CreateUri(variant));


        [DataRow(TestType.TestType1, 1)]
        [DataRow(TestType.TestType2, 2)]
        [DataRow(TestType.TestType3, 3)]
        [DataRow(TestType.TestType4, 4)]
        [TestMethod] public void GetDropDownEntryUriTest(TestType variant, int expected) 
            => Assert.AreEqual(obj.CreateUri(variant), obj.GetDropDownEntryUri(expected - 1));

        [DataRow(TestType.TestType1, 1)]
        [DataRow(TestType.TestType2, 2)]
        [DataRow(TestType.TestType3, 3)]
        [DataRow(TestType.TestType4, 4)]
        [TestMethod] public void GetDropDownEntryNameTest(TestType variant, int expected) 
            => Assert.AreEqual(variant.ToStr(), obj.GetDropDownEntryName(expected - 1));
        

    }
}