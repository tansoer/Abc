using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class FactoredUnitTests : SealedTests<FactoredUnit, Unit> {

        private double factorsCount;
        private UnitFactorsRepo factors;
        private MeasuresRepo measures;
        private UnitFactor siSystemFactor;
        private Unit o;
        private string expectedShort;
        private string expectedLong;
        private string expectedMeasure;

        protected override FactoredUnit createObject() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Factored;

            return new FactoredUnit(d);
        }

        protected static FunctionedUnit createFunctioned() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Functioned;

            return new FunctionedUnit(d);
        }

        private static Unit createDerived() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.MeasureType = MeasureType.Derived;
            var m = new DerivedMeasure(d);
            new derivedMeasureTestData(m).add();

            var ud = GetRandom.ObjectOf<UnitData>();
            ud.UnitType = UnitType.Derived;
            ud.MeasureId = m.Id;
            var u = new DerivedUnit(ud, m);
            new derivedUnitTestData(u).add();

            return u;
        }

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            factorsCount = GetRandom.UInt8(15, 30);
            factors = GetRepo.Instance<IUnitFactorsRepo>() as UnitFactorsRepo;
            measures = GetRepo.Instance<IMeasuresRepo>() as MeasuresRepo;
            addAll(obj);
        }

        [TestCleanup]
        public override void TestCleanup() {
            validate(o);
            removeAll(measures?.dbSet, measures?.db);
            removeAll(factors?.dbSet, factors?.db);

            factors = null;
            measures = null;

            base.TestCleanup();
        }

        private void validate(Unit u) {
            if (u is null) return;
            Assert.IsNotNull(u);
            Assert.IsInstanceOfType(u, typeof(DerivedUnit));
            Assert.AreEqual(expectedShort, u.Formula());
            Assert.AreEqual(expectedLong, u.Formula(true));
            Assert.AreEqual(expectedMeasure, u.Measure.Formula());
            Assert.AreEqual(u.Id, u.Formula());
            Assert.AreNotEqual(string.Empty, u.Id);
        }

        [TestMethod]
        public async Task FactorsTest() {
            removeAll(factors?.dbSet, factors?.db);
            await testListAsync<UnitFactor, UnitFactorData, IUnitFactorsRepo>(
                d => d.UnitId = obj.Id, d => new UnitFactor(d));
        }


        [TestMethod]
        public async Task MeasureTest() {
            removeAll(measures?.dbSet, measures?.db);
            await testItemAsync<MeasureData, Measure, IMeasuresRepo>(
                obj.MeasureId, () => obj.Measure.Data, MeasureFactory.Create);
        }

        [TestMethod] public void SiSystemFactorTest() => Assert.IsNotNull(obj.SiSystemFactor);

        [TestMethod] public void FactorTest() => Assert.AreEqual(siSystemFactor.Factor, obj.Factor);

        [TestMethod]
        public void ToBaseTest() {
            var d = GetRandom.Double(-10000, 10000);
            Assert.AreEqual(d * siSystemFactor.Factor, obj.ToBase(d));
        }

        [TestMethod]
        public void FromBaseTest() {
            var d = GetRandom.Double(-10000, 10000);
            Assert.AreEqual(d / siSystemFactor.Factor, obj.FromBase(d));
        }

        [TestMethod]
        public void FormulaTest() {
            Assert.AreEqual($"{1/obj.Factor}", obj.Formula());
            Assert.AreEqual($"{1/obj.Factor}", obj.Formula(true));
        }

        [TestMethod]
        public void InverseTest() {
            o = obj.Inverse();
            expectedShort = $"{obj.Code}^-1";
            expectedLong = $"{obj.Name}^-1";
            expectedMeasure = $"{obj.Measure.Code}^-1";
        }

        [TestMethod]
        public void PowerTest() {
            var i = GetRandom.Int32(-10, 10);
            o = obj.Power(i);
            expectedShort = i == 0 ? Word.Unspecified : i == 1 ? obj.Code : $"{obj.Code}^{i}";
            expectedLong = i == 0 ? Word.Unspecified : i == 1 ? obj.Name : $"{obj.Name}^{i}";
            expectedMeasure = i == 0 ? Word.Unspecified : i == 1 ? obj.Measure.Code : $"{obj.Measure.Code}^{i}";
        }

        [TestMethod]
        public void MultiplyTest() {
            var u = createObject();
            addAll(u);
            o = obj.Multiply(u);
            expectedShort = $"{obj.Code} * {u.Code}";
            expectedLong = $"{obj.Name} * {u.Name}";
            expectedMeasure = $"{obj.Measure.Code} * {u.Measure.Code}";
        }

        [TestMethod]
        public void MultiplyWithFunctionedTest() {
            var u = createFunctioned();
            addMeasure(u);
            o = obj.Multiply(u);
            expectedShort = $"{obj.Code} * {u.Code}";
            expectedLong = $"{obj.Name} * {u.Name}";
            expectedMeasure = $"{obj.Measure.Code} * {u.Measure.Code}";
        }

        [TestMethod]
        public void MultiplyWithDerivedTest() {
            addMeasure(obj);
            var u = createDerived();
            o = obj.Multiply(u);
            expectedShort = $"{obj.Code} * ua * uc^2 * ub^-1 * ud^-2";
            expectedLong = $"{obj.Name} * uua * uuc^2 * uub^-1 * uud^-2";
            expectedMeasure = $"{obj.Measure.Code} * a * c^2 * b^-1 * d^-2";
        }

        [TestMethod]
        public void DivideTest() {
            var u = createObject();
            addMeasure(u);
            o = obj.Divide(u);
            expectedShort = $"{obj.Code} * {u.Code}^-1";
            expectedLong = $"{obj.Name} * {u.Name}^-1";
            expectedMeasure = $"{obj.Measure.Code} * {u.Measure.Code}^-1";
        }

        [TestMethod]
        public void DivideWithFunctionedTest() {
            var u = createFunctioned();
            addMeasure(u);
            o = obj.Divide(u);
            expectedShort = $"{obj.Code} * {u.Code}^-1";
            expectedLong = $"{obj.Name} * {u.Name}^-1";
            expectedMeasure = $"{obj.Measure.Code} * {u.Measure.Code}^-1";
        }

        [TestMethod]
        public void DivideWithDerivedTest() {
            addMeasure(obj);
            var u = createDerived();
            o = obj.Divide(u);
            expectedShort = $"{obj.Code} * ub * ud^2 * ua^-1 * uc^-2";
            expectedLong = $"{obj.Name} * uub * uud^2 * uua^-1 * uuc^-2";
            expectedMeasure = $"{obj.Measure.Code} * b * d^2 * a^-1 * c^-2";
        }

        private void addAll(Unit u) {
            addMeasure(u);
            addFactors(u);
        }

        private void addMeasure(Unit u) {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.Id = u.MeasureId;
            d.MeasureType = MeasureType.Base;
            measures.Add(new BaseMeasure(d));

        }

        private void addFactors(Unit u) {

            for (var i = 0; i < factorsCount; i++) {
                var d = GetRandom.ObjectOf<UnitFactorData>();
                d.Factor = GetRandom.Double(-10, 10);
                if (i % 4 == 0) d.UnitId = u.Id;
                if (i % 8 == 0 && i < 10 && i > 1)
                    d.SystemOfUnitsId = Abc.Core.Units.SystemOfUnits.SiSystemId;
                var f = new UnitFactor(d);
                factors.Add(f);
                if (f.SystemOfUnitsId == Abc.Core.Units.SystemOfUnits.SiSystemId)
                    siSystemFactor = f;
            }
        }

    }

}
