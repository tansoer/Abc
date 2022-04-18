using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Quantities;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Quantities {

    [TestClass] public class UnitsPageTests : SealedViewsFactoryPageTests<
        UnitsPage,
        IUnitsRepo, 
        Unit, 
        UnitView, 
        UnitData, 
        UnitViewFactory,
        UnitType> {
        internal class unitsRepo : mockRepo<Unit, UnitData>, IUnitsRepo {}
        private class measuresRepo : mockRepo<Measure, MeasureData>, IMeasuresRepo {}
        private unitsRepo units;
        private measuresRepo measures;
        private MeasureData data;
        private IUnitFactorsRepo factors { get; } = getRepo<IUnitFactorsRepo>();
        private IUnitRulesRepo rules { get; } = getRepo<IUnitRulesRepo>();
        private IUnitTermsRepo terms { get; } = getRepo<IUnitTermsRepo>();

        protected override Unit toObject(UnitData d) {
            if (d.UnitType == UnitType.Error) d.UnitType = UnitType.Factored;
            else if (d.UnitType == UnitType.Unspecified) d.UnitType = UnitType.Derived;
            return UnitFactory.Create(d);
        }
        protected override string pageTitle => "Units";
        protected override string pageUrl => "/Quantities/Units";
        protected override UnitsPage createObject() {
            units = new unitsRepo();
            measures = new measuresRepo();
            data = GetRandom.ObjectOf<MeasureData>();
            var m = MeasureFactory.Create(data);
            measures.Add(m);
            addRandomMeasures();
            addRandomTerms();
            addRandomFactors();
            addRandomRules();
            return new UnitsPage(units);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(measures, units);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomFactors(int? count = null, string id = null) {
            count ??= GetRandom.UInt8(5, 10);
            for (var i = 0; i < count; i++) {
                var d = random<UnitFactorData>();
                if (id != null) d.UnitId = id;
                factors.Add(new UnitFactor(d));
            }
        }
        private void addRandomRules(int? count = null, string id = null) {
            count ??= GetRandom.UInt8(5, 10);
            for (var i = 0; i < count; i++) {
                var d = random<UnitRulesData>();
                if (id != null) d.UnitId = id;
                rules.Add(new UnitRules(d));
            }
        }
        private void addRandomTerms(int? count = null, string id = null) {
            count ??= GetRandom.UInt8(5, 10);
            for (var i = 0; i < count; i++) {
                var d = random<CommonTermData>();
                if (id != null) d.MasterId = id;
                terms.Add(new UnitTerm(d));
            }
        }
        private void addRandomMeasures() {
            for (var i = 0; i < GetRandom.UInt8(3, 10); i++) {
                var d = random<MeasureData>();
                var m = MeasureFactory.Create(d);
                measures.Add(m);
            }
        }
        [TestMethod]
        public void MeasureNameTest() {
            var name = obj.MeasureName(data.Id);
            Assert.AreEqual(data.Name, name);
        }
        [TestMethod]
        public void MeasuresTest() {
            var list = measures.Get();
            Assert.AreEqual(list.Count + 1, obj.Measures.Count());
        }
        [TestMethod]
        public void LoadDetailsTest() {
            obj.LoadDetails();
            areEqual(0, obj.Terms.Count);
            areEqual(0, obj.Factors.Count);
            areEqual(0, obj.UnitRules.Count);
            var view = random<UnitView>();
            var countTerms = random(2, 5);
            var countFactors = random(2, 5);
            var countRules = random(2, 5);
            addRandomTerms(countTerms, view.Id);
            addRandomFactors(countFactors, view.Id);
            addRandomRules(countRules, view.Id);
            obj.Item = view;
            obj.LoadDetails();
            areEqual(countTerms, obj.Terms.Count);
            areEqual(countFactors, obj.Factors.Count);
            areEqual(countRules, obj.UnitRules.Count);
        }
        [TestMethod] public void UnitTypesTest() {
            dynamic l = obj.UnitTypes;
            isInstanceOfType(l, typeof(SelectList));
            areEqual(GetEnum.Count<UnitType>(), l.Items.Length);
        }
        [TestMethod] public void TermsTest() {
            isInstanceOfType(obj.Terms, typeof(List<UnitTermView>));
            areEqual(0, obj.Terms.Count);
        }
        [TestMethod] public void FactorsTest() {
            isInstanceOfType(obj.Factors, typeof(List<UnitFactorView>));
            areEqual(0, obj.Factors.Count);
        }
        [TestMethod] public void UnitRulesTest() {
            isInstanceOfType(obj.UnitRules, typeof(List<UnitRulesView>));
            areEqual(0, obj.UnitRules.Count);
        }
        [TestMethod] public void ButtonsCountTest() {
            areEqual(4, obj.ButtonsCount);
            obj.Buttons.Clear();
            areEqual(0, obj.ButtonsCount);
        }
        [DataRow(0, "Index", "Select")]
        [DataRow(1, "Edit", "Edit")]
        [DataRow(2, "Details", "Details")]
        [DataRow(3, "Delete", "Delete")]
        [TestMethod] public void GetButtonTest(int i, string expectedAction, string expectedCaption) {
            var actual = obj.GetButton(i);
            areEqual(actual?.Action, expectedAction);
            areEqual(actual?.Caption, expectedCaption);
        }
        [TestMethod] public override void ToObjectTest() {
            var view = random<UnitView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data, nameof(view.Formula));
        }
        [TestMethod]
        public override void ToViewTest() {
            var d = random<UnitData>();
            var o = toObject(d);
            var view = obj.toView(o);
            allPropertiesAreEqual(view, o.Data, nameof(view.Formula));
            areEqual(view.Formula, o.Formula(true));
        }
    }
}

