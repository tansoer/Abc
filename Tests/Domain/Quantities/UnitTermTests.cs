using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Infra.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class UnitTermTests : SealedTests<UnitTerm, BaseTerm> {

        private UnitsRepo units;
        private QuantityDb db;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            obj = new UnitTerm(GetRandom.ObjectOf<CommonTermData>());
            units = GetRepo.Instance<IUnitsRepo>() as UnitsRepo;
            db = units?.db as QuantityDb;
        }

        public override void TestCleanup() {
            removeAll(db?.Units, db);
            units = null;
            db = null;
            base.TestCleanup();
        }

        [TestMethod] public void MasterUnitIdTest() => isReadOnly(obj.Data.MasterId);

        [TestMethod] public void TermUnitIdTest() => isReadOnly(obj.Data.TermId);

        [TestMethod]
        public void MasterUnitTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Derived;
            d.Id = obj.MasterUnitId;
            units.Add(new DerivedUnit(d));
            allPropertiesAreEqual(d, obj.MasterUnit.Data);
        }

        [TestMethod]
        public void TermUnitTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Factored;
            d.Id = obj.TermUnitId;
            units.Add(new FactoredUnit(d));
            allPropertiesAreEqual(d, obj.TermUnit.Data);
        }

        [TestMethod]
        public void FormulaTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Factored;
            d.Id = obj.TermUnitId;
            units.Add(new FactoredUnit(d));

            if (obj.Power != 1) {
                Assert.AreEqual($"{d.Code}^{obj.Power}", obj.Formula());
                Assert.AreEqual($"{d.Name}^{obj.Power}", obj.Formula(true));
            }
            else {
                Assert.AreEqual($"{d.Code}", obj.Formula());
                Assert.AreEqual($"{d.Name}", obj.Formula(true));
            }
        }

    }

}