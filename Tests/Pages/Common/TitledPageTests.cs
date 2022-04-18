using Abc.Aids.Random;
using Abc.Pages.Common;
using Abc.Tests.Data;
using Abc.Tests.Domain;
using Abc.Tests.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Common {

    [TestClass]
    public class TitledPageTests
        : AbstractPageTests<TitledPage<TestPage, ITestRepo, TestObject, TestView, TestData>
            ,SortedPage<TestPage, ITestRepo, TestObject, TestView, TestData>> {

        protected override TitledPage<TestPage, ITestRepo, TestObject, TestView, TestData> createObject() {
             return new testClass(new testRepo());
        }

        [TestMethod]
        public void ItemIdTest() {
            obj.Item = GetRandom.ObjectOf<TestView>();
            Assert.AreEqual(obj.Item.Id, obj.ItemId);
        }

        [TestMethod] public void TitleTest() => isReadOnly(obj.Title);
        
        [TestMethod] public void SubTitleTest() => isReadOnly(obj.SubTitle);
        

        [TestMethod] public void GetSubtitleTest() 
            => Assert.AreEqual(obj.SubTitle, obj.subtitle);

        [TestMethod]
        public void IsMasterDetailTest() {
            Assert.IsFalse(obj.IsMasterDetail());
            ((testClass) obj).subTitle = rndStr;
            obj.FixedValue = rndStr;
            Assert.IsTrue(obj.IsMasterDetail());
        }

    }

}
