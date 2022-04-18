using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Facade.Classifiers;
using Abc.Pages.Classifiers;
using Abc.Pages.Common;
using Abc.Pages.Orders;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Tests.Pages.Classifiers {
    [TestClass] public class ClassifierPageTests: AbstractPageTests<
        ClassifierPage<SalesChannelsPage>,
        ViewFactoryPage<SalesChannelsPage, IClassifiersRepo, IClassifier, ClassifierView, ClassifierData, ClassifierViewFactory>> {
        internal class classifierRepo :mockRepo<IClassifier, ClassifierData>, IClassifiersRepo { }
        [TestMethod] public void BaseTypesTest() {
            var list = repo.Get().Where(x => x.IsTypeOf(fixedClassifierType)).ToList();
            Assert.AreEqual(list.Count + 1, obj.BaseTypes.Count());
        }
        [TestMethod] public void BaseTypeNameTest() {
            var randomBaseType = repo.Get()[0];
            var name = obj.BaseTypeName(randomBaseType.Id);
            Assert.AreEqual(name, randomBaseType.Name);
        }
        [TestMethod] public async Task OnGetIndexAsyncTest() {
            isNull(obj.Items);
            await obj.OnGetIndexAsync(null, null, null, null, null, null, null);
            areEqual(count, obj.Items.Count);
        }
        [TestMethod] public void OnGetCreateTest() {
            var sortOrder = rndStr;
            var searchString = rndStr;
            var pageIndex = GetRandom.UInt8();
            var fixedFilter = rndStr;
            var fixedValue = rndStr;
            var createSwitch = GetRandom.UInt8(0, 10);

            var page = obj.OnGetCreate(sortOrder, searchString,
                pageIndex, fixedFilter, fixedValue, createSwitch);
            Assert.IsInstanceOfType(page, typeof(PageResult));
            pagePropertiesTest(sortOrder, searchString, pageIndex, fixedFilter, 
                fixedValue);
        }
        private int count;
        private classifierRepo repo;
        private const ClassifierType fixedClassifierType = ClassifierType.SalesChannel;
        internal static int addRandomItems(IClassifiersRepo r, ClassifierType t) {
            var cnt = GetRandom.UInt8(10, 100);
            for (var i = 0; i < cnt; i++) {
                var c = GetRandom.UInt8(0, 5);
                r.Add(createTestObject(t));
                for (var j = 0; j < c; j++)
                    r.Add(createOtherObject(t));
            }
            return cnt;
        }
        protected override ClassifierPage<SalesChannelsPage> createObject() {
            repo = new classifierRepo();
            count = addRandomItems(repo, fixedClassifierType);
            return new SalesChannelsPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private static IClassifier createTestObject(ClassifierType t) {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = t;
            return ClassifierFactory.Create(d);
        }
        private static IClassifier createOtherObject(ClassifierType t) {
            var d = GetRandom.ObjectOf<ClassifierData>();
            if (d.ClassifierType == t)
                d.ClassifierType = ClassifierType.Unspecified;
            return ClassifierFactory.Create(d);
        }
        private void pagePropertiesTest(string sortOrder, string searchString,
            int pageIdx, string fixedFilter, string fixedValue, string selId = null) {
            areEqual(selId, obj.SelectedId);
            areEqual(fixedFilter, obj.FixedFilter);
            areEqual(fixedValue, obj.FixedValue);
            areEqual(searchString, obj.SearchString);
            areEqual(pageIdx, obj.PageIndex);
            areEqual(sortOrder, obj.SortOrder);
        }
    }
}
