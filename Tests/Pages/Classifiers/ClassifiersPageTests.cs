using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Classifiers;
using Abc.Domain.Classifiers;
using Abc.Domain.Common;
using Abc.Facade.Classifiers;
using Abc.Pages.Classifiers;
using Abc.Pages.Common;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Classifiers {
    [TestClass]
    public class ClassifiersPageTests :SealedViewsPageTests<
        ClassifiersPage,
        IClassifiersRepo,
        IClassifier,
        ClassifierView,
        ClassifierData,
        ClassifierType> {

        protected override Type getBaseClass() =>
            typeof(ViewsPage<ClassifiersPage, IClassifiersRepo,
                IClassifier, ClassifierView, ClassifierData, ClassifierType>);
        protected override string pageUrl => ClassifierUrls.Classifiers;
        protected override string pageTitle => ClassifierTitles.Classifiers;
        private class testRepo
            :mockRepo<IClassifier, ClassifierData>,
                IClassifiersRepo { }
        private testRepo repo;

        protected override ClassifiersPage createObject() {
            repo = new testRepo();
            addRandomClassificators();
            return new ClassifiersPage(repo);
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
        protected override IClassifier toObject(ClassifierData d) => ClassifierFactory.Create(d);

        private void addRandomClassificators() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                repo.Add(createClassificator(ClassifierType.Order));
            }
        }
        private static IClassifier createClassificator(ClassifierType t) {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = t;
            return ClassifierFactory.Create(d);
        }
        [TestMethod]
        public void OnGetCreateTest() {
            var page = obj.OnGetCreate(sortOrder, searchString, pageIndex,
                fixedFilter, fixedValue, createSwitch);
            Assert.IsInstanceOfType(page, typeof(PageResult));
            testPageProperties();
        }
        [DataTestMethod]
        [DataRow(ClassifierType.Order)]
        public virtual void ToObjectTest(ClassifierType t) {
            var v = GetRandom.ObjectOf<ClassifierView>();
            v.ClassifierType = t;
            var o = obj.toObject(v);
            allPropertiesAreEqual(o.Data, v);
        }
        [DataTestMethod]
        [DataRow(ClassifierType.Order)]
        public virtual void ToViewTest(ClassifierType t) {
            var d = GetRandom.ObjectOf<ClassifierData>();
            d.ClassifierType = t;
            var v = obj.toView(toObject(d));
            allPropertiesAreEqual(d, v);
        }

        [TestMethod]
        public void BaseTypesTest() {
            Assert.IsInstanceOfType(obj.BaseTypes, typeof(List<SelectListItem>));
            Assert.AreEqual(repo.list.Count + 1, obj.BaseTypes.Count());
        }

        [TestMethod]
        public void TypesTest() {
            Assert.IsInstanceOfType(obj.BaseTypes, typeof(List<SelectListItem>));
            Assert.AreEqual(Enum.GetValues<ClassifierType>().Length, obj.Types.Count());
        }

        [TestMethod]
        public void ValueForTest() {
            var mock = new mockHtmlHelper<ClassifiersPage>();
            Assert.IsNull(obj.ValueFor(mock, 0));
            Assert.IsNull(obj.ValueFor(mock, 1));
        }
    }

}
