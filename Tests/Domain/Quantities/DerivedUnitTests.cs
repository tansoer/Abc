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
    public class DerivedUnitTests : SealedTests<DerivedUnit, Unit> {

        private UnitTermsRepo terms;
        private UnitsRepo units;
        private UnitFactorsRepo factors;
        private Unit o;
        private string expectedShort;
        private string expectedLong;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            units = GetRepo.Instance<IUnitsRepo>() as UnitsRepo;
            terms = GetRepo.Instance<IUnitTermsRepo>() as UnitTermsRepo;
            factors = GetRepo.Instance<IUnitFactorsRepo>() as UnitFactorsRepo;
        }
        [TestCleanup]
        public override void TestCleanup() {
            validate(o);
            removeAll(units?.dbSet, units?.db);
            removeAll(terms?.dbSet, terms?.db);
            removeAll(factors?.dbSet, factors?.db);
            units = null;
            terms = null;
            factors = null;
            base.TestCleanup();
        }

        private void validate(Unit u) {
            if (u is null) return;
            Assert.IsNotNull(u);
            Assert.IsInstanceOfType(u, typeof(DerivedUnit));
            Assert.AreEqual(expectedShort, u.Formula());
            Assert.AreEqual(expectedLong, u.Formula(true));
            Assert.AreEqual(u.Id, u.Formula());
            Assert.AreNotEqual(string.Empty, u.Id);
        }

        [TestMethod]
        public void ToBaseTest() {
            var d = GetRandom.Double(-100, 100);
            var testData = new derivedUnitTestData(obj, true);
            testData.add();
            var x = obj.ToBase(d);
            d = testData.toBase(d);
            Assert.AreEqual(d, x, 0.00000000001);
        }
        [TestMethod]
        public void FromBaseTest() {
            var d = GetRandom.Double(-100, 100);
            var testData = new derivedUnitTestData(obj, true);
            testData.add();
            var x = obj.FromBase(d);
            d = testData.fromBase(d);
            Assert.AreEqual(d, x, 0.00000000001);
        }
        [TestMethod]
        public void FormulaTest() {
            new derivedUnitTestData(obj).add();
            Assert.AreEqual("ua * uc^2 * ub^-1 * ud^-2", obj.Formula());
            Assert.AreEqual("uua * uuc^2 * uub^-1 * uud^-2", obj.Formula(true));
        }
        [TestMethod]
        public void InverseTest() {
            new derivedUnitTestData(obj).add();
            o = obj.Inverse();
            expectedShort = "ub * ud^2 * ua^-1 * uc^-2";
            expectedLong = "uub * uud^2 * uua^-1 * uuc^-2";
        }
        [TestMethod]
        public void PowerTest() {
            new derivedUnitTestData(obj).add();
            var i = GetRandom.Int32(3, 10);
            o = obj.Power(i);
            expectedShort = $"ua^{i} * uc^{2 * i} * ub^{-i} * ud^{-2 * i}";
            expectedLong = $"uua^{i} * uuc^{2 * i} * uub^{-i} * uud^{-2 * i}";
        }
        [TestMethod]
        public void PowerZeroTest() {
            new derivedUnitTestData(obj).add();
            o = obj.Power(0);
            expectedShort = Word.Unspecified;
            expectedLong = Word.Unspecified;
        }
        [TestMethod]
        public void MultiplyTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Factored;
            new derivedUnitTestData(obj).add();
            o = obj.Multiply(new FactoredUnit(d));
            expectedShort = $"ua * {d.Code} * uc^2 * ub^-1 * ud^-2";
            expectedLong = $"uua * {d.Name} * uuc^2 * uub^-1 * uud^-2";
        }
        [TestMethod]
        public void MultiplyWithFunctionedTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Functioned;
            new derivedUnitTestData(obj).add();
            o = obj.Multiply(new FunctionedUnit(d));
            expectedShort = $"ua * {d.Code} * uc^2 * ub^-1 * ud^-2";
            expectedLong = $"uua * {d.Name} * uuc^2 * uub^-1 * uud^-2";
        }
        [TestMethod]
        public void MultiplyWithDerivedTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Derived;
            new derivedUnitTestData(obj).add();
            var u = new DerivedUnit(d);
            new derivedUnitTestData(u).add();
            o = obj.Multiply(u);
            expectedShort = "ua^2 * uc^4 * ub^-2 * ud^-4";
            expectedLong = "uua^2 * uuc^4 * uub^-2 * uud^-4";
        }
        [TestMethod]
        public void DivideTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Factored;
            new derivedUnitTestData(obj).add();
            o = obj.Divide(new FactoredUnit(d));
            expectedShort = $"ua * uc^2 * ub^-1 * {d.Code}^-1 * ud^-2";
            expectedLong = $"uua * uuc^2 * uub^-1 * {d.Name}^-1 * uud^-2";
        }
        [TestMethod]
        public void DivideWithFunctionedTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Functioned;
            new derivedUnitTestData(obj).add();
            o = obj.Divide(new FunctionedUnit(d));
            expectedShort = $"ua * uc^2 * ub^-1 * {d.Code}^-1 * ud^-2";
            expectedLong = $"uua * uuc^2 * uub^-1 * {d.Name}^-1 * uud^-2";
        }
        [TestMethod]
        public void DivideWithDerivedTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Derived;
            new derivedUnitTestData(obj).add();
            var u = new DerivedUnit(d);
            new derivedUnitTestData(u).add();
            o = obj.Divide(u);
            expectedShort = Word.Unspecified;
            expectedLong = Word.Unspecified;
        }
        [TestMethod]
        public void TermsTest() {
            var ud = GetRandom.ObjectOf<UnitData>();
            ud.UnitType = UnitType.Derived;
            obj = new DerivedUnit(ud);
            var count = GetRandom.UInt8(10, 30);

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<CommonTermData>();
                if (i % 4 == 0) d.MasterId = obj.Id;
                terms.Add(new UnitTerm(d));
            }

            var t = isReadOnly(obj, nameof(obj.Terms)) as IReadOnlyList<UnitTerm>;
            Assert.IsNotNull(t);
            Assert.AreEqual(Math.Ceiling(count / 4.0), t.Count);
        }

    }

}
