using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Attributes;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Parties {

    [TestClass] public class BodyMetricTypesPageTests : SealedViewFactoryPageTests<
        BodyMetricTypesPage, 
        IBodyMetricTypesRepo,
        BodyMetricType, 
        BodyMetricTypeView, 
        BodyMetricTypeData, 
        BodyMetricTypeViewFactory> {
        private class testRepo : mockRepo<BodyMetricType, BodyMetricTypeData>, IBodyMetricTypesRepo { }
        private testRepo repo;
        protected override BodyMetricTypesPage createObject() {
            repo = new testRepo();
            addRandomTypes();
            return new BodyMetricTypesPage(repo);
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
        private void addRandomTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                repo.Add(new BodyMetricType(GetRandom.ObjectOf<BodyMetricTypeData>()));
        }
        protected override string pageTitle => PartyTitles.BodyMetricTypes;
        protected override string pageUrl => PartyUrls.BodyMetricTypes;
        protected override BodyMetricType toObject(BodyMetricTypeData d) => new (d);
        [TestMethod] public void BaseTypeNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.BaseTypeName(rndStr));
            var m = repo.list[0];
            Assert.AreEqual(m.Data.Name, obj.BaseTypeName(m.Data.Id));
        }
        [TestMethod] public void BaseTypesTest() {
            Assert.IsInstanceOfType(obj.BaseTypes, typeof(List<SelectListItem>));
            Assert.AreEqual(repo.list.Count + 1, obj.BaseTypes.Count());
        }
    }
}
