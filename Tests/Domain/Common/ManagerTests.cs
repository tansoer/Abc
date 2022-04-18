using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Domain.Common;
using Abc.Facade.Common;
using Abc.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Common {
    [TestClass]
    public class ManagerTests :AbstractTests<Manager<ITestRepo, TestObject>, Entity<ManagerData>> {
        private class testClass :Manager<ITestRepo, TestObject> {
            public testClass() : this(null) { }
            public testClass(ManagerData d) : base(d) { }

        }
        protected override Manager<ITestRepo, TestObject> createObject()
            => new testClass(random<ManagerData>());

        [TestMethod] public void RepoTest() => isReadOnly();
        
        [TestMethod] public void AddTest() {
            var beforeCount = getCurrentItemsCount();
            obj.Add(getRandomTestObject());
            var afterCount = getCurrentItemsCount();
            areEqual(beforeCount + 1, afterCount);
        }

        [TestMethod]
        public void DeleteTest() {
            var beforeCount = getCurrentItemsCount();
            var testObject = getRandomTestObject();
            obj.Add(testObject);
            obj.Delete(testObject);
            var afterCount = getCurrentItemsCount();
            areEqual(beforeCount, afterCount);
        }

        [TestMethod]
        public void UpdateTest() {
            var testObject = getRandomTestObject();
            string testObjectId = testObject.Id;
            obj.Add(testObject);

            var replacementData = GetRandom.ObjectOf<TestData>();
            replacementData.Id = testObjectId;

            obj.Update(new(replacementData));
            
            var actualObject = obj.Repo.Get(testObjectId);
            allPropertiesAreEqual(replacementData, actualObject.Data);
        }

        [TestMethod]
        public void FindByIdTest() {
            var testObject = getRandomTestObject();
            obj.Add(testObject);
            var actual = obj.FindById(testObject.Id);
            allPropertiesAreEqual(testObject.Data, actual.Data);
        }

        [TestMethod]
        public void findTest() {
            string randomSearchString = GetRandom.String(50, 100);
            int searchableCount = GetRandom.UInt8(1, 10);

            for (int i = 0; i < searchableCount; i++) {
                var searchableTestData = GetRandom.ObjectOf<TestData>();
                searchableTestData.Name = randomSearchString;
                obj.Add(new(searchableTestData));
            }

            for (int i = 0; i < searchableCount; i++) {
                var randomFillerData = getRandomTestObject();
                obj.Add(randomFillerData);
            }
            
            int resultCount = obj.find<ITestRepo, TestObject>(randomSearchString).Count;
            areEqual(searchableCount, resultCount);
        }

        private TestObject getRandomTestObject() => new(GetRandom.ObjectOf<TestData>());
        private int getCurrentItemsCount() => obj.Repo.Get().Count;
    }
}
