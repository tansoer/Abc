using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {
    [TestClass] public class BaseMeasureTests :SealedTests<BaseMeasure, Measure> {

        private Measure o;
        private string expectedShort;
        private string expectedLong;
        private MeasureTermsRepo terms;
        private MeasuresRepo measures;

        protected override BaseMeasure createObject() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.MeasureType = MeasureType.Base;
            return new BaseMeasure(d);
        }

        private static Measure createDerived() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.MeasureType = MeasureType.Derived;
            var m = new DerivedMeasure(d);
            new derivedMeasureTestData(m).add();
            return m;
        }

        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            terms = GetRepo.Instance<IMeasureTermsRepo>() as MeasureTermsRepo;
            measures = GetRepo.Instance<IMeasuresRepo>() as MeasuresRepo;
        }

        [TestCleanup] public override void TestCleanup() {
            validate(o);
            removeAll(terms?.dbSet, terms?.db);
            removeAll(measures?.dbSet, measures?.db);
            terms = null;
            measures = null;
            expectedLong = null;
            expectedShort = null;
            base.TestCleanup();
        }

        private void validate(Measure m) {
            if (m is null) return;
            Assert.IsNotNull(m);
            Assert.IsInstanceOfType(m, typeof(DerivedMeasure));
            Assert.AreEqual(expectedShort, m.Formula());
            Assert.AreEqual(expectedLong, m.Formula(true));
            Assert.AreEqual(m.Id, m.Formula());
            Assert.AreNotEqual(string.Empty, m.Id);
        }

        [TestMethod] public void DivideTest() {
            var m = createObject();
            o = obj.Divide(m);
            expectedShort = $"{obj.Code} * {m.Code}^-1";
            expectedLong = $"{obj.Name} * {m.Name}^-1";
        }

        [TestMethod] public void MultiplyTest() {
            var m = createObject();
            o = obj.Multiply(m);
            expectedShort = $"{obj.Code} * {m.Code}";
            expectedLong = $"{obj.Name} * {m.Name}";
        }

        [TestMethod] public void DivideWithDerivedTest() {
            var m = createDerived();
            o = obj.Divide(m);
            expectedShort = $"{obj.Code} * b * d^2 * a^-1 * c^-2";
            expectedLong = $"{obj.Name} * bb * dd^2 * aa^-1 * cc^-2";
        }

        [TestMethod] public void MultiplyWithDerivedTest() {
            var m = createDerived();
            o = obj.Multiply(m);
            expectedShort = $"{obj.Code} * a * c^2 * b^-1 * d^-2";
            expectedLong = $"{obj.Name} * aa * cc^2 * bb^-1 * dd^-2";
        }

        [TestMethod] public void InverseTest() {
            o = obj.Inverse();
            expectedShort = $"{obj.Code}^-1";
            expectedLong = $"{obj.Name}^-1";
        }

        [TestMethod] public void PowerTest() {
            var i = GetRandom.Int32(-10, 10);
            o = obj.Power(i);
            expectedShort = powerFormula(i, obj.Code);
            expectedLong = powerFormula(i, obj.Name);
        }

        private static string powerFormula(int i, string n) => i switch {
            0 => Word.Unspecified,
            1 => n,
            _ => $"{n}^{i}"
        };

        [TestMethod] public void FormulaTest() {
            Assert.AreEqual(obj.Code, obj.Formula());
            Assert.AreEqual(obj.Name, obj.Formula(true));
        }
    }
}
