using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class MeasureTermTests : SealedTests<MeasureTerm, BaseTerm> {

        private MeasureData dMaster;
        private MeasureData dTerm;
        private MeasuresRepo r;

        protected override MeasureTerm createObject()
            => new MeasureTerm(GetRandom.ObjectOf<CommonTermData>());

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            dMaster = GetRandom.ObjectOf<MeasureData>();
            fixData(dMaster, obj.MasterMeasureId, MeasureType.Derived);
            dTerm = GetRandom.ObjectOf<MeasureData>();
            fixData(dTerm, obj.TermMeasureId, MeasureType.Base);
            r = GetRepo.Instance<IMeasuresRepo>() as MeasuresRepo;
        }

        [TestMethod] public void MasterMeasureIdTest() => isReadOnly(obj.Data.MasterId);

        [TestMethod] public void TermMeasureIdTest() => isReadOnly(obj.Data.TermId);

        [TestMethod]
        public void MasterMeasureTest() {
            r.Add(MeasureFactory.Create(dMaster));
            var m = isReadOnly() as Measure;
            Assert.IsNotNull(m);
            allPropertiesAreEqual(dMaster, m.Data);
        }

        [TestMethod]
        public void TermMeasureTest() {
            r.Add(MeasureFactory.Create(dTerm));
            var m = isReadOnly() as Measure;
            Assert.IsNotNull(m);
            allPropertiesAreEqual(dTerm, m.Data);
        }

        [TestMethod]
        public void FormulaTest() {
            Assert.AreEqual($"{obj.TermMeasure.Code}^{obj.Power}", obj.Formula());
            Assert.AreEqual($"{obj.TermMeasure.Name}^{obj.Power}", obj.Formula(true));
        }

        [TestMethod]
        public void InternalFormulaTest() {
            var s = rndStr;
            var actual = obj.formula(s);
            var expected = $"{s}^{obj.Power}";
            Assert.AreEqual(expected, actual);
        }

        private static void fixData(MeasureData d, string id, MeasureType t) {
            d.Id = id;
            d.MeasureType = t;
        }

    }

}