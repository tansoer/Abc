using System;
using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Core.Rounding;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass] public class QuantityTests : 
        SealedTests<Quantity, MeasurableValue<Quantity, double, Unit>> {
        private UnitData unitData;
        private DerivedUnit unit;
        private double amount;
        private Quantity quantity;
        private Quantity q1;
        private Quantity q2;
        private Quantity q3;
        private Quantity sameQ1;
        private Quantity greaterQ1;
        private Quantity lessQ1;
        private UnitsRepo units;
        private QuantityDb db;
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            units = GetRepo.Instance<IUnitsRepo>() as UnitsRepo;
            db = units?.db as QuantityDb;
            unitData = GetRandom.ObjectOf<UnitData>();
            unit = new DerivedUnit(unitData);
            amount = GetRandom.Double(-1000, 1000);
            quantity = new Quantity(amount, unit);
            createData();
        }
        [TestCleanup] public override void TestCleanup() {
            removeAll(db?.SystemsOfUnits, db);
            removeAll(db?.Measures, db);
            removeAll(db?.CommonTerms, db);
            removeAll(db?.Units, db);
            removeAll(db?.UnitFactors, db);
            removeAll(db?.UnitRules, db);
            base.TestCleanup();
        }
        [TestMethod] public void PascalToMmHgTest() {
            TestCleanup();
            QuantityDbInitializer.Initialize(db);
            var q = new Quantity(1, Abc.Core.Units.Pressure.pascalName);
            var mmHg = units.Get(Abc.Core.Units.Pressure.mmHgName);
            var qMmHg = q.ConvertTo(mmHg);
            mostlyEqual(133.32239, qMmHg.Amount);
        }
        [TestMethod] public void MmHgToPascalTest() {
            TestCleanup();
            QuantityDbInitializer.Initialize(db);
            var q = new Quantity(1, Abc.Core.Units.Pressure.mmHgName);
            var pa = units.Get(Abc.Core.Units.Pressure.pascalName);
            var qPa = q.ConvertTo(pa);
            mostlyEqual(0.0075006156130264, qPa.Amount);
        }
        [TestMethod] public void AddTest() {
            testAdd(q1, q2);
            testAdd(q2, q1);
            testAdd(q1, q3, false);
            testAdd(q3, q1, false);
        }
        [TestMethod] public void AmountTest() => isReadOnly(double.NaN);
        [TestMethod] public void CantChangeUnitProperties() {
            quantity.Unit.Data.Name = rndStr;
            quantity.Unit.Data.MeasureId = rndStr;
            quantity.Unit.Data.Code = rndStr;
            quantity.Unit.Data.Details = rndStr;
            quantity.Unit.Data.Id = rndStr;
            quantity.Unit.Data.ValidFrom = rndDt;
            quantity.Unit.Data.ValidTo = rndDt;
            allPropertiesAreEqual(unit.Data, quantity.Unit.Data);
        }
        [TestMethod] public void CompareToTest() {
            testCompareTo(q1, q1, 0);
            testCompareTo(q1.Add(new Quantity(1, q1.Unit.Id)), q1, 1);
            testCompareTo(q1.Add(new Quantity(-1, q1.Unit.Id)), q1, -1);
            testCompareTo(q1, q3, int.MaxValue);
            testCompareTo(q1, null, int.MinValue);
        }
        [DataRow(2.0000000000000000001, 2, 0)]
        [DataRow(2, 2, 0)]
        [DataRow(3, 2, 1)]
        [DataRow(2, 3, -1)]
        [DataTestMethod] public void CompareAlmostEqualTest(double q1, double q2, int expected) {
            areEqual(expected, Quantity.CompareAlmostEqual(q1, q2));
        }
        [TestMethod] public void ConvertToTest() {
            testConvertTo(q1, q1.Unit);
            testConvertTo(q1, q2.Unit);
            testConvertTo(q2, q1.Unit);
            testConvertTo(q3, q1.Unit, false);
            testConvertTo(q3, null, false);
        }
        [TestMethod] public void DivideByScalarTest() {
            testDivideByScalar(q1, GetRandom.Double(-1000, 1000));
            testDivideByScalar(q1.Inverse(), GetRandom.Double(-1000, 1000));
            testDivideByScalar(q1, double.NaN);
            testDivideByScalar(q1, double.Epsilon);
            testDivideByScalar(q1, double.MaxValue);
            testDivideByScalar(q1, double.MinValue);
            testDivideByScalar(q1, double.PositiveInfinity);
            testDivideByScalar(q1, double.NegativeInfinity);
            testDivideByScalar(q1, 0);
        }
        [TestMethod] public void DivideTest() {
            testDivide(q1, q2);
            testDivide(q1.Inverse(), q1);
            testDivide(q1, q3);
            testDivide(q1, q1);
            testDivide(q1, null, false);
            testDivide(q1, new Quantity(0, q2.Unit));
            testDivide(q1, new Quantity(0, q1.Unit));
        }
        [TestMethod] public void EmptyTest() {
            var x1 = Quantity.Unspecified;
            var x2 = Quantity.Unspecified;
            Assert.AreNotSame(x1, x2);
            Assert.AreEqual(x1.Amount, x2.Amount);
            Assert.AreEqual(x1.Unit.Id, x2.Unit.Id);
            Assert.AreEqual(double.NaN, x2.Amount);
            Assert.AreEqual(Word.Unspecified, x2.Unit.Id);
        }
        [TestMethod] public void InverseTest() => testInverse(q1, q1.Unit.Inverse());
        [TestMethod] public void IsEqualTest() {
            Assert.IsTrue(obj.IsEqual(obj));
            Assert.IsTrue(q1.IsEqual(q1));
            Assert.IsTrue(q2.IsEqual(q2));
            Assert.IsTrue(q3.IsEqual(q3));
            Assert.IsFalse(obj.IsEqual(q1));
            Assert.IsFalse(obj.IsEqual(q2));
            Assert.IsFalse(obj.IsEqual(q3));
        }
        [TestMethod] public void IsGreaterTest() {
            Assert.IsFalse(q1.IsGreater(sameQ1), $"{q1} != {sameQ1.ConvertTo(q1.Unit)}");
            Assert.IsFalse(q1.IsGreater(greaterQ1), $"{q1}={sameQ1.ConvertTo(q1.Unit)} > {greaterQ1.ConvertTo(q1.Unit)}");
            Assert.IsTrue(q1.IsGreater(lessQ1), $"{q1}={sameQ1.ConvertTo(q1.Unit)} < {lessQ1.ConvertTo(q1.Unit)}");
        }
        [TestMethod] public void IsLessTest() {
            Assert.IsFalse(q1.IsLess(sameQ1), $"{q1} != {sameQ1.ConvertTo(q1.Unit)}");
            Assert.IsTrue(q1.IsLess(greaterQ1), $"{q1}={sameQ1.ConvertTo(q1.Unit)} > {greaterQ1.ConvertTo(q1.Unit)}");
            Assert.IsFalse(q1.IsLess(lessQ1), $"{q1}={sameQ1.ConvertTo(q1.Unit)} < {lessQ1.ConvertTo(q1.Unit)}");
        }
        [TestMethod] public void IsSameMeasureTest() {
            Assert.IsTrue(q1.IsSameMeasure(q2.Unit));
            Assert.IsTrue(q1.IsSameMeasure(q1.Unit));
            Assert.IsFalse(q1.IsSameMeasure(q3.Unit));
            Assert.IsFalse(q1.IsSameMeasure(null));
        }
        [TestMethod] public void MultiplyByScalarTest() {
            testMultiplyByScalar(q1, GetRandom.Double(-1000, 1000));
            testMultiplyByScalar(q1.Inverse(), GetRandom.Double(-1000, 1000));
            testMultiplyByScalar(q1, double.NaN);
            testMultiplyByScalar(q1, double.Epsilon);
            testMultiplyByScalar(q1, double.MaxValue);
            testMultiplyByScalar(q1, double.MinValue);
            testMultiplyByScalar(q1, double.PositiveInfinity);
            testMultiplyByScalar(q1, double.NegativeInfinity);
            testMultiplyByScalar(q1, 0);
        }
        [TestMethod] public void MultiplyTest() {
            testMultiply(q1, q2);
            testMultiply(q1.Inverse(), q1);
            testMultiply(q1, q3);
            testMultiply(q1, q1);
            testMultiply(q1, null, false);
            testMultiply(q1, new Quantity(0, q2.Unit));
            testMultiply(q1, new Quantity(0, q1.Unit));
        }
        [TestMethod] public void OppositeTest() {
            var q = q1.Opposite();
            Assert.AreEqual(q.Amount, -q1.Amount);
            Assert.AreEqual(q.Unit.Id, q1.Unit.Id);
        }
        [TestMethod] public void ParseTest() {
            var u = UnitFactory.Create(GetRandom.ObjectOf<UnitData>());
            GetRepo.Instance<IUnitsRepo>().Add(u);
            var d = GetRandom.Double(-1000000, 1000000);
            var s = $"{d} {u.Id}";
            var q = Quantity.Parse(s);
            Assert.AreEqual(d, q.Amount);
            allPropertiesAreEqual(u.Data, q.Unit.Data);
        }
        [TestMethod] public void PowerTest() {
            testPower(q1, GetRandom.Int32(-100, 0));
            testPower(q1.Inverse(), GetRandom.Int32(1, 100));
            testPower(q1.Inverse(), 0);
        }
        [TestMethod] public void RoundTest() {
            roundTest(4.5, new RoundingPolicy(RoundingStrategy.RoundUp, 1), 4.45);
            roundTest(4.46, new RoundingPolicy(RoundingStrategy.RoundUp, 2), 4.456);
            roundTest(-4.5, new RoundingPolicy(RoundingStrategy.RoundUp, 1), -4.45);
            roundTest(-4.46, new RoundingPolicy(RoundingStrategy.RoundUp, 2), -4.456);
            roundTest(1400, new RoundingPolicy(RoundingStrategy.RoundUp, 2), 1400.00);
            roundTest(4.4, new RoundingPolicy(RoundingStrategy.RoundDown, 1), 4.45);
            roundTest(4.45, new RoundingPolicy(RoundingStrategy.RoundDown, 2), 4.456);
            roundTest(-4.4, new RoundingPolicy(RoundingStrategy.RoundDown, 1), -4.45);
            roundTest(-4.45, new RoundingPolicy(RoundingStrategy.RoundDown, 2), -4.456);
            roundTest(1400, new RoundingPolicy(RoundingStrategy.RoundDown, 1), 1400.00);
            roundTest(4.5, new RoundingPolicy(RoundingStrategy.Round, 1, 5), 4.45);
            roundTest(4.45, new RoundingPolicy(RoundingStrategy.Round, 2, 7), 4.456);
            roundTest(-4.5, new RoundingPolicy(RoundingStrategy.Round, 1, 5), -4.45);
            roundTest(-4.45, new RoundingPolicy(RoundingStrategy.Round, 2, 7), -4.456);
            roundTest(0.01, new RoundingPolicy(RoundingStrategy.Round, 5, 5), 0.01);
            roundTest(0.01, new RoundingPolicy(RoundingStrategy.Round, 5, 5), 0.0100000000002);
            roundTest(1, new RoundingPolicy(RoundingStrategy.Round, 0, 5), 0.99999999999999989);
            roundTest(4.5, new RoundingPolicy(RoundingStrategy.RoundUpByStep, 0.25), 4.45);
            roundTest(-4.5, new RoundingPolicy(RoundingStrategy.RoundUpByStep, 0.25), -4.45);
            roundTest(4.25, new RoundingPolicy(RoundingStrategy.RoundDownByStep, 0.25), 4.45);
            roundTest(-4.25, new RoundingPolicy(RoundingStrategy.RoundDownByStep, 0.25), -4.45);
            roundTest(4.5, new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 1), 4.45);
            roundTest(4.46, new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 2), 4.456);
            roundTest(-4.4, new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 1), -4.45);
            roundTest(-4.45, new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 2), -4.456);
            roundTest(4.4, new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 1), 4.45);
            roundTest(4.45, new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 2), 4.456);
            roundTest(-4.5, new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 1), -4.45);
            roundTest(-4.46, new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 2), -4.456);
        }
        [TestMethod] public void SqrtTest() {
            var q = new Quantity(Math.Abs(q1.Amount), q1.Unit);
            q = q.Sqrt();
            Assert.AreEqual(q.Amount, Math.Sqrt(Math.Abs(q1.Amount)));
            var toCompare = q.Unit.Id;
            if (q.Unit.Id != q1.Unit.Id + "^0.5" && q.Unit.Id.Contains(',')) toCompare = q.Unit.Id.Replace(',', '.');
            Assert.AreEqual(toCompare, q1.Unit.Id+ "^0.5");
        }
        [TestMethod] public void SubtractTest() {
            testSubtract(q1, q2);
            testSubtract(q2, q1);
            testSubtract(q1, q3, false);
            testSubtract(q3, q1, false);
        }
        [TestMethod] public void ToBaseTest() => Assert.AreEqual(q1.ToBase(), q1.Unit.ToBase(q1.Amount));
        [TestMethod] public void ToStringTest() => Assert.AreEqual($"{amount} {unitData.Code}", quantity.ToString());
        [TestMethod] public void ToStringUsesIdWhenNoCodeAndNameTest() {
            unitData.Code = null;
            unitData.Name = null;
            quantity = new Quantity(amount, new DerivedUnit(unitData));
            Assert.AreEqual($"{amount} {unitData.Id}", quantity.ToString());
        }
        [TestMethod] public void ToStringUsesNameWhenNoCodeTest() {
            unitData.Code = null;
            quantity = new Quantity(amount, new DerivedUnit(unitData));
            Assert.AreEqual($"{amount} {unitData.Name}", quantity.ToString());
        }
        [TestMethod] public void ToStringUsesUndefinedWhenNoCodeNameAndIdTest() {
            unitData.Code = null;
            unitData.Name = null;
            unitData.Id = null;
            quantity = new Quantity(amount, new DerivedUnit(unitData));
            Assert.AreEqual($"{amount} {Word.Unspecified}", quantity.ToString());
        }
        [TestMethod] public void ToStringUsesUndefinedWhenNoUnitDataTest() {
            quantity = new Quantity(amount, new DerivedUnit());
            Assert.AreEqual($"{amount} {Word.Unspecified}", quantity.ToString());
        }
        [TestMethod] public void ToStringUsesUndefinedWhenNoUnitTest() {
            quantity = new Quantity(amount, (Unit) null);
            Assert.AreEqual($"{amount} {Word.Unspecified}", quantity.ToString());
        }
        [TestMethod] public void TryParseTest() {
            var u = UnitFactory.Create(GetRandom.ObjectOf<UnitData>());
            GetRepo.Instance<IUnitsRepo>().Add(u);
            var d = GetRandom.Double(-1000000, 1000000);
            var s = $"{d} {u.Id}";
            var b = Quantity.TryParse(s, out var q);
            Assert.AreEqual(true, b);
            Assert.AreEqual(d, q.Amount);
            allPropertiesAreEqual(u.Data, q.Unit.Data);
        }
        [TestMethod] public void UnitTest() {
            var u = UnitFactory.Create(GetRandom.ObjectOf<UnitData>());
            var x = new Quantity(GetRandom.Double(), u);
            allPropertiesAreEqual(u.Data, x.Unit.Data);
            GetRepo.Instance<IUnitsRepo>().Add(u);
            x = new Quantity(GetRandom.Double(), u.Id);
            Assert.AreNotSame(u, x.Unit);
            allPropertiesAreEqual(u.Data, x.Unit.Data);
        }
        [TestMethod] public void UnspecifiedTest() {
            var q = Quantity.Unspecified;
            Assert.AreEqual(double.NaN, q.Amount);
            Assert.AreEqual(Word.Unspecified, q.Unit.Id);
        }
        private void createData() {
            var md = GetRandom.ObjectOf<MeasureData>();
            md.MeasureType = MeasureType.Derived;
            var m = new DerivedMeasure(md);
            var testMeasure = new derivedMeasureTestData(m);
            testMeasure.add();
            var ud = GetRandom.ObjectOf<UnitData>();
            ud.MeasureId = m.Id;
            ud.UnitType = UnitType.Derived;
            var u = new DerivedUnit(ud, m);
            var testData = new derivedUnitTestData(u, true);
            testData.add();
            q1 = new Quantity(GetRandom.Int32(-1000, 1000), "ua1");
            q2 = new Quantity(GetRandom.Int32(-1000, 1000), "ua2");
            q3 = new Quantity(GetRandom.Int32(-1000, 1000), "ub");
            sameQ1 = q1.ConvertTo(q2.Unit);
            greaterQ1 = q1.Add(new Quantity(GetRandom.Int32(1, 1000), q1.Unit)).ConvertTo(q2.Unit);
            lessQ1 = q1.Subtract(new Quantity(GetRandom.Int32(1, 1000), q1.Unit)).ConvertTo(q2.Unit);
        }
        private static void isSpecified(Quantity q, in double expected, string unitId) {
            Assert.AreEqual(expected, q.Amount);
            Assert.AreEqual(unitId, q.Unit.Id);
        }
        private static void isUnspecified(Quantity x) {
            Assert.AreEqual(double.NaN, x.Amount);
            Assert.AreEqual(Word.Unspecified, x.Unit.Id);
        }
        private void roundTest(double expected, RoundingPolicy p, double a) {
            var u = UnitFactory.Create(GetRandom.ObjectOf<UnitData>());
            obj = new Quantity(a, u);
            var q = obj.Round(p);
            Assert.AreNotSame(obj, q);
            Assert.AreEqual(expected, q.Amount);
            allPropertiesAreEqual(u.Data, q.Unit.Data);
        }
        private static void testAdd(Quantity a, Quantity b, bool hasValue = true) {
            var x = a.Add(b);
            if (hasValue) {
                var d1 = a.Unit.ToBase(a.Amount);
                var d2 = b.Unit.ToBase(b.Amount);
                var expected = b.Unit.FromBase(d1 + d2);
                isSpecified(x, expected, b.Unit.Id);
            }
            else { isUnspecified(x); }
        }
        private static void testCompareTo(Quantity a, Quantity b, int expected) =>
            Assert.AreEqual(expected, a.CompareTo(b));
        private static void testConvertTo(Quantity q, Unit u, bool hasValue = true) {
            var x = q.ConvertTo(u);
            if (hasValue) {
                var d = q.Unit.ToBase(q.Amount);
                var expected = u.FromBase(d);
                isSpecified(x, expected, u.Id);
            }
            else { isUnspecified(x); }
        }
        private static void testDivide(Quantity a, Quantity b, bool hasValue = true) {
            var x = a.Divide(b);
            if (hasValue) {
                var u = a.Unit.Divide(b.Unit);
                var d1 = a.ToBase();
                var d2 = b.ToBase();
                var expected = u.FromBase(d1 / d2);
                isSpecified(x, expected, u.Id);
            }
            else { isUnspecified(x); }
        }
        private static void testDivideByScalar(Quantity a, double b) {
            var x = a.Divide(b);
            var expected = a.Amount / b;
            Assert.AreEqual(expected, x.Amount);
            Assert.AreEqual(a.Unit.Id, x.Unit.Id);
        }
        private void testInverse(Quantity q, Unit u) {
            var x = q.Inverse();
            var r = Quantity.CompareAlmostEqual(1 / q.Amount, x.Amount);
            Assert.AreEqual(0, r);
            Assert.AreEqual(u.Id, x.Unit.Id);
        }
        private static void testMultiply(Quantity a, Quantity b, bool hasValue = true) {
            var x = a.Multiply(b);
            if (hasValue) {
                var u = a.Unit.Multiply(b.Unit);
                var d1 = a.ToBase();
                var d2 = b.ToBase();
                var expected = u.FromBase(d1 * d2);
                isSpecified(x, expected, u.Id);
            }
            else { isUnspecified(x); }
        }
        private static void testMultiplyByScalar(Quantity a, double b) {
            var x = a.Multiply(b);
            var expected = a.Amount * b;
            Assert.AreEqual(expected, x.Amount);
            Assert.AreEqual(a.Unit.Id, x.Unit.Id);
        }
        private static void testPower(Quantity q, int power) {
            var x = q.Power(power);
            var u = q.Unit.Power(power);
            var expected = Math.Pow(q.Amount, power);
            Assert.AreEqual(expected, x.Amount);
            Assert.AreEqual(u.Id, x.Unit.Id);
        }
        private static void testSubtract(Quantity a, Quantity b, bool hasValue = true) {
            var x = a.Subtract(b);
            if (hasValue) {
                var d1 = a.Unit.ToBase(a.Amount);
                var d2 = b.Unit.ToBase(b.Amount);
                var expected = b.Unit.FromBase(d1 - d2);
                isSpecified(x, expected, b.Unit.Id);
            }
            else { isUnspecified(x); }
        }
    }
}