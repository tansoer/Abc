using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Quantities;
using Abc.Tests.Pages.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Quantities {

    [TestClass] public class MeasuresPageTests : SealedViewsFactoryPageTests<
        MeasuresPage, 
        IMeasuresRepo,
        Measure, 
        MeasureView, 
        MeasureData,
        MeasureViewFactory,
        MeasureType> {
        private class measuresRepo : mockRepo<Measure, MeasureData>, IMeasuresRepo { }
        private class termsRepo : mockRepo<MeasureTerm, CommonTermData>, IMeasureTermsRepo { }
        private class unitsRepo :mockRepo<Unit, UnitData>, IUnitsRepo { }
        private measuresRepo measures;
        private termsRepo terms;
        private unitsRepo units;
        protected override string pageTitle => "Measures";
        protected override string pageUrl => "/Quantities/Measures";
        protected override Measure toObject(MeasureData d) {
            if (d.MeasureType == MeasureType.Error) d.MeasureType = MeasureType.Base;
            else if (d.MeasureType == MeasureType.Unspecified) d.MeasureType = MeasureType.Derived;
            return MeasureFactory.Create(d);
        }
        protected override MeasuresPage createObject() {
            measures = new measuresRepo();
            terms = new termsRepo();
            units = new unitsRepo();
            addRandomTerms();
            addRandomUnits();
            return new MeasuresPage(measures);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(measures, terms, units);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomTerms(int? count = null, string measureId = null) {
            count ??= GetRandom.UInt8(5, 10);
            for (var i = 0; i < count; i++) {
                var d = random<CommonTermData>();
                if (measureId is not null) d.MasterId = measureId;
                terms.Add(new MeasureTerm(d));
            }
        }
        private void addRandomUnits(int? count = null, string measureId = null) {
            count ??= GetRandom.UInt8(5, 10);
            for (var i = 0; i < count; i++) {
                var d = random<UnitData>();
                if (measureId is not null) d.MeasureId = measureId;
                units.Add(UnitFactory.Create(d));
            }
        }
        [TestMethod] public void UnitsTest() {
            Assert.IsInstanceOfType(obj.Units, typeof(List<UnitView>));
            Assert.AreEqual(0, obj.Units.Count);
        }
        [TestMethod] public void LoadDetailsTest() {
            obj.LoadDetails();
            Assert.AreNotEqual(0, terms.list.Count);
            Assert.AreNotEqual(0, units.list.Count);
            Assert.AreEqual(0, obj.MeasureTerms.Count);
            Assert.AreEqual(0, obj.Units.Count);
            var view = GetRandom.ObjectOf<MeasureView>();
            var count = GetRandom.UInt8(2, 5);
            addRandomTerms(count, view.Id);
            addRandomUnits(count+count, view.Id);
            obj.Item = view;
            obj.LoadDetails();
            Assert.AreEqual(count, obj.MeasureTerms.Count);
            Assert.AreEqual(count+count, obj.Units.Count);
        }
        [TestMethod] public override void ToObjectTest() {
            var view = GetRandom.ObjectOf< MeasureView>();
            var o = obj.toObject(view);
            allPropertiesAreEqual(view, o.Data, nameof(view.Formula));
        }
        [TestMethod] public override void ToViewTest() {
            var d = GetRandom.ObjectOf<MeasureData>();
            var o = toObject(d);
            var view = obj.toView(o);
            allPropertiesAreEqual(view, o.Data, nameof(view.Formula));
            areEqual(view.Formula, o.Formula(true));
        }
        [TestMethod] public void ButtonsCountTest() {
            Assert.AreEqual(5, obj.ButtonsCount);
            obj.Buttons.Clear();
            Assert.AreEqual(0, obj.ButtonsCount);
        }
        [DataRow(0, "Index", "Select")]
        [DataRow(1, "Edit", "Edit")]
        [DataRow(2, "Details", "Details")]
        [DataRow(3, "Delete", "Delete")]
        [DataRow(4, "Index", "Units")]
        [DataTestMethod] public void GetButtonTest(int i, string expectedAction, string expectedCaption) {
            var actual = obj.GetButton(i);
            Assert.AreEqual(actual?.Action, expectedAction);
            Assert.AreEqual(actual?.Caption, expectedCaption);
        }
        [TestMethod]
        public void MeasureTermsTest() => isProperty<List<MeasureTermView>>();
        [TestMethod]
        public void MeasuresTest() {
            var list = measures.Get();
            Assert.AreEqual(list.Count + 1, obj.Measures.Count());
        }
    }
}
