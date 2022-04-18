using System;
using System.Collections.Generic;
using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class DerivedMeasureTests : SealedTests<DerivedMeasure, Measure> {

        private MeasureTermsRepo terms;
        private MeasuresRepo measures;
        private Measure o;
        private string expectedShort;
        private string expectedLong;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();

            terms = GetRepo.Instance<IMeasureTermsRepo>() as MeasureTermsRepo;
            measures = GetRepo.Instance<IMeasuresRepo>() as MeasuresRepo;
        }

        [TestCleanup]
        public override void TestCleanup() {
            validate(o);
            removeAll(terms?.dbSet, terms?.db);
            removeAll(measures?.dbSet, measures?.db);
            terms = null;
            measures = null;
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

        [TestMethod]
        public void DivideTest() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.MeasureType = MeasureType.Base;
            new derivedMeasureTestData(obj).add();
            o = obj.Divide(new BaseMeasure(d));
            expectedShort = $"a * c^2 * b^-1 * {d.Code}^-1 * d^-2";
            expectedLong = $"aa * cc^2 * bb^-1 * {d.Name}^-1 * dd^-2";
        }

        [TestMethod]
        public void DivideByDerivedTest() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.MeasureType = MeasureType.Derived;
            new derivedMeasureTestData(obj).add();
            var m = new DerivedMeasure(d);
            new derivedMeasureTestData(m).add();
            o = obj.Divide(m);
            expectedShort = Word.Unspecified;
            expectedLong = Word.Unspecified;
        }


        [TestMethod]
        public void MultiplyTest() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.MeasureType = MeasureType.Base;
            new derivedMeasureTestData(obj).add();
            o = obj.Multiply(new BaseMeasure(d));
            expectedShort = $"a * {d.Code} * c^2 * b^-1 * d^-2";
            expectedLong = $"aa * {d.Name} * cc^2 * bb^-1 * dd^-2";
        }

        [TestMethod]
        public void MultiplyByDerivedTest() {
            var d = GetRandom.ObjectOf<MeasureData>();
            d.MeasureType = MeasureType.Derived;
            new derivedMeasureTestData(obj).add();
            var m = new DerivedMeasure(d);
            new derivedMeasureTestData(m).add();
            o = obj.Multiply(m);
            expectedShort = $"a^2 * c^4 * b^-2 * d^-4";
            expectedLong = $"aa^2 * cc^4 * bb^-2 * dd^-4";
        }


        [TestMethod]
        public void InverseTest() {
            new derivedMeasureTestData(obj).add();
            o = obj.Inverse();
            expectedShort = "b * d^2 * a^-1 * c^-2";
            expectedLong = "bb * dd^2 * aa^-1 * cc^-2";
        }

        [TestMethod]
        public void PowerTest() {
            new derivedMeasureTestData(obj).add();
            var i = GetRandom.Int32(3, 10);
            o = obj.Power(i);
            expectedShort = $"a^{i} * c^{2 * i} * b^{-i} * d^{-2 * i}";
            expectedLong = $"aa^{i} * cc^{2 * i} * bb^{-i} * dd^{-2 * i}";
        }

        [TestMethod]
        public void PowerZeroTest() {
            new derivedMeasureTestData(obj).add();
            o = obj.Power(0);
            expectedShort = Word.Unspecified;
            expectedLong = Word.Unspecified;
        }

        [TestMethod]
        public void TermsTest() {
            var md = GetRandom.ObjectOf<MeasureData>();
            md.MeasureType = MeasureType.Derived;
            obj = new DerivedMeasure(md);
            var count = GetRandom.UInt8(10, 30);

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<CommonTermData>();
                if (i % 4 == 0) d.MasterId = obj.Id;
                terms.Add(new MeasureTerm(d));
            }

            var t = isReadOnly(obj, nameof(obj.Terms)) as IReadOnlyList<MeasureTerm>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
        }

        [TestMethod]
        public void FormulaTest() {
            new derivedMeasureTestData(obj).add();
            Assert.AreEqual("a * c^2 * b^-1 * d^-2", obj.Formula());
            Assert.AreEqual("aa * cc^2 * bb^-1 * dd^-2", obj.Formula(true));
        }
    }
}
