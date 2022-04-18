using System;
using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Facade.Quantities;
using Abc.Pages.Quantities;
using Abc.Tests.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Quantities {

    [TestClass]
    public class MeasureTermsPageTests :SealedViewFactoryPageTests<
        MeasureTermsPage,
        IMeasureTermsRepo,
        MeasureTerm, 
        MeasureTermView, 
        CommonTermData,
        MeasureTermViewFactory> {
        private class measuresRepo :mockRepo<Measure, MeasureData>, IMeasuresRepo {}
        private class termsRepo :mockRepo<MeasureTerm, CommonTermData>, IMeasureTermsRepo {}
        private measuresRepo measures;
        private termsRepo terms;
        protected override string pageTitle => obj.Title;
        protected override string pageUrl => QuantityUrls.MeasureTerms;
        protected override MeasureTerm toObject(CommonTermData d) => new (d);
        protected override MeasureTermsPage createObject() {
            measures = new measuresRepo();
            terms = new termsRepo();
            addRandomMeasures();
            return new MeasureTermsPage(terms);
        }
        private IServiceProvider services;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            services = GetRepo.services;
            GetRepo.services = new mockServiceProvider(measures, terms);
        }
        [TestCleanup]
        public override void TestCleanup() {
            GetRepo.services = services;
            base.TestCleanup();
        }
        private void addRandomMeasures() {
            for (var i = 0; i < GetRandom.UInt8(5, 10); i++)
                measures.Add(MeasureFactory.Create(GetRandom.ObjectOf<MeasureData>()));
        }
        [TestMethod] public void MeasureNameTest() {
            Assert.AreEqual(Word.Unspecified, obj.MeasureName(rndStr));
            var m = measures.list[0];
            Assert.AreEqual(m.Data.Name, obj.MeasureName(m.Data.Id));
        }
        [TestMethod] public void MeasuresTest() {
            Assert.IsInstanceOfType(obj.Measures, typeof(List<SelectListItem>));
            Assert.AreEqual(measures.list.Count + 1, obj.Measures.Count());
        }
    }
}
