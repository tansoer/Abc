using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Quantities {

    [TestClass] public class QuantityDbInitializerTests :DbInitializerTests<QuantityDb> {

        public QuantityDbInitializerTests() {
            type = typeof(QuantityDbInitializer);
            db = new QuantityDb(options);
            QuantityDbInitializer.Initialize(db);
        }
        private void clearAll() {
            removeAll(db.CommonTerms);
            removeAll(db.Measures);
            removeAll(db.UnitFactors);
            removeAll(db.Units);
            removeAll(db.SystemsOfUnits);
            removeAll(db.UnitRules);
        }

        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void MeasuresTest() => areEqual(17, db.Measures.Count());
        [TestMethod] public void CommonTermsTest() => areEqual(66, db.CommonTerms.Count());
        [TestMethod] public void UnitFactorsTest() => areEqual(90, db.UnitFactors.Count());
        [TestMethod] public void UnitsTest() => areEqual(134, db.Units.Count());
        [TestMethod] public void SystemsOfUnitsTest() => areEqual(2, db.SystemsOfUnits.Count());
        [TestMethod] public void UnitRulesTest() => areEqual(3, db.UnitRules.Count());

        [TestMethod] public void InitializeDataTest() {
            QuantityDbInitializer.initialize(Abc.Core.Units.SystemOfUnits.Units);
            Assert.AreEqual(2, db.SystemsOfUnits.Count());
        }

        [TestMethod] public void InitializeMeasureTest() {
            clearAll();
            QuantityDbInitializer.initialize(Abc.Core.Units.Area.Measure, Abc.Core.Units.Area.Units);
            Assert.AreEqual(18, db.CommonTerms.Count());
            Assert.AreEqual(1, db.Measures.Count());
            Assert.AreEqual(0, db.UnitFactors.Count());
            Assert.AreEqual(16, db.Units.Count());
            Assert.AreEqual(0, db.SystemsOfUnits.Count());
        }

        [TestMethod] public void AddUnitFactorsTest() {
            clearAll();
            var count = GetRandom.UInt8(5, 15);
            var l = new List<Abc.Core.Units.UnitInfo>();

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<Abc.Core.Units.UnitInfo>();
                d.Factor = GetRandom.Double(-10000, 10000);
                l.Add(d);
            }

            var s = rndStr;
            QuantityDbInitializer.addUnitFactors(l, s);
            Assert.AreEqual(0, db.CommonTerms.Count());
            Assert.AreEqual(0, db.Measures.Count());
            Assert.AreEqual(count, db.UnitFactors.Count());
            Assert.AreEqual(0, db.Units.Count());
            Assert.AreEqual(0, db.SystemsOfUnits.Count());
            foreach (var e in db.UnitFactors) Assert.AreEqual(s, e.SystemOfUnitsId);
        }

        [TestMethod] public void AddTermsTest() {
            clearAll();
            var expected = GetRandom.ObjectOf<Abc.Core.Units.UnitInfo>();
            var count = GetRandom.UInt8(5, 15);

            for (var i = 0; i < count; i++)
                expected.Terms.Add(GetRandom.ObjectOf<Abc.Core.Units.TermInfo>());
            QuantityDbInitializer.addTerms(expected, db.CommonTerms);
            db.SaveChanges();
            Assert.AreEqual(count, db.CommonTerms.Count());
            Assert.AreEqual(0, db.Measures.Count());
            Assert.AreEqual(0, db.UnitFactors.Count());
            Assert.AreEqual(0, db.Units.Count());
            Assert.AreEqual(0, db.SystemsOfUnits.Count());
        }

        [TestMethod] public void AddItemTermsTest() {
            clearAll();
            var expected = new List<Abc.Core.Units.UnitInfo>();
            var count = GetRandom.UInt8(5, 15);

            for (var i = 0; i < count; i++) {
                var d = GetRandom.ObjectOf<Abc.Core.Units.UnitInfo>();
                for (var j = 0; j < count; j++)
                    d.Terms.Add(GetRandom.ObjectOf<Abc.Core.Units.TermInfo>());
                expected.Add(d);
            }

            QuantityDbInitializer.addTerms(expected);
            Assert.AreEqual(count * count, db.CommonTerms.Count());
            Assert.AreEqual(0, db.Measures.Count());
            Assert.AreEqual(0, db.UnitFactors.Count());
            Assert.AreEqual(0, db.Units.Count());
            Assert.AreEqual(0, db.SystemsOfUnits.Count());
        }

        [TestMethod] public void AddMeasureTest() {
            clearAll();
            var expected = GetRandom.ObjectOf<Abc.Core.Units.UnitInfo>();
            QuantityDbInitializer.addMeasure(expected);
            Assert.AreEqual(0, db.CommonTerms.Count());
            Assert.AreEqual(1, db.Measures.Count());
            Assert.AreEqual(0, db.UnitFactors.Count());
            Assert.AreEqual(0, db.Units.Count());
            Assert.AreEqual(0, db.SystemsOfUnits.Count());
            var actual = db.Measures.First();
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Code, actual.Code);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Definition, actual.Details);
        }

        [TestMethod] public void GetItemTest() {
            QuantityDbInitializer.Initialize(db);
            var expected = GetRandom.ObjectOf<MeasureData>();
            db.Measures.Add(expected);
            db.SaveChanges();
            var actual = QuantityDbInitializer.getItem(db.Measures, expected.Id);
            allPropertiesAreEqual(expected, actual);
        }

        [TestMethod] public void AddUnitsTest() {
            clearAll();
            var s = rndStr;
            QuantityDbInitializer.addUnits(Abc.Core.Units.Counter.Units, s);
            Assert.AreEqual(0, db.CommonTerms.Count());
            Assert.AreEqual(0, db.Measures.Count());
            Assert.AreEqual(0, db.UnitFactors.Count());
            Assert.AreEqual(7, db.Units.Count());
            Assert.AreEqual(0, db.SystemsOfUnits.Count());

            foreach (var e in db.Units) Assert.AreEqual(s, e.MeasureId);
        }
    }
}
