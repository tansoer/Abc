using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Quantities;
using Abc.Facade.Parties;
using Abc.Pages.Parties;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Parties {
    [TestClass]
    public class BodyMetricPageTests :
        SealedViewsFactoryPageTests<BodyMetricPage, 
            IBodyMetricsRepo,
            IBodyMetric,
            BodyMetricView,
            BodyMetricData,
            BodyMetricViewFactory,
            MetricType> {
        private class testRepo :mockRepo<IBodyMetric, BodyMetricData>, IBodyMetricsRepo { }
        private class typesRepo :mockRepo<BodyMetricType, BodyMetricTypeData>, IBodyMetricTypesRepo { }
        private class unitsRepo :mockRepo<Unit, UnitData>, IUnitsRepo { }
        private class partiesRepo :mockRepo<IParty, PartyData>, IPartiesRepo { }

        private testRepo repo;
        private typesRepo types;
        private unitsRepo units;
        private partiesRepo parties;

        protected override BodyMetricPage createObject() {
            repo = new testRepo();
            types = new typesRepo();
            units = new unitsRepo();
            parties = new partiesRepo();
            addRandomBodyMetrics();
            addRandomTypes();
            return new BodyMetricPage(repo);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(repo, types, units, parties);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }

        private void addRandomBodyMetrics() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                repo.Add(createBodyMetric(MetricType.String));
                repo.Add(createBodyMetric(MetricType.Quantity));
                repo.Add(createBodyMetric(MetricType.Unspecified));
            }
        }

        private static IBodyMetric createBodyMetric(MetricType t) {
            var d = GetRandom.ObjectOf<BodyMetricData>();
            d.MetricType = t;
            return BodyMetricFactory.Create(d);
        }

        private void addRandomTypes() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++) {
                types.Add(createType());
            }
        }

        private BodyMetricType createType(string id = null) {
            var d = GetRandom.ObjectOf<BodyMetricTypeData>();
            if (id != null) d.Id = id;
            return new BodyMetricType(d);
        }

        protected override string pageTitle => PartyTitles.BodyMetrics;
        protected override string pageUrl => PartyUrls.BodyMetrics;

        protected override IBodyMetric toObject(BodyMetricData d)
            => BodyMetricFactory.Create(d);

        [TestMethod]
        public override void ToObjectTest() {
            var view = GetRandom.ObjectOf<BodyMetricView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data, "StringValue", "QuantityValue", "Value");
        }
        [TestMethod]
        public override void ToViewTest() {
            var d = GetRandom.ObjectOf<BodyMetricData>();
            var view = obj.toView(toObject(d));
            allPropertiesAreEqual(view, d, "StringValue", "QuantityValue", "Value");
        }

        [TestMethod]
        public void TypesTest() {
            isReadOnly();
            Assert.IsInstanceOfType(obj.Types, typeof(List<SelectListItem>));
            Assert.AreEqual(obj.Types.Count() - 1, types.list.Count);
        }
        [TestMethod]
        public void UnitsTest() {
            isReadOnly();
            Assert.IsInstanceOfType(obj.Units, typeof(List<SelectListItem>));
            Assert.AreEqual(obj.Units.Count() - 1, units.list.Count);
        }
    }
}