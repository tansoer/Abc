using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass] public class UnitAttributeTests: 
        AbstractTests<UnitAttribute<UnitFactorData>, BaseEntity<UnitFactorData>> {
        private class testClass : UnitAttribute<UnitFactorData> {
            public testClass(UnitFactorData d) : base(d) { }
        }
        protected override UnitAttribute<UnitFactorData> createObject() 
            => new testClass(random<UnitFactorData>());
        [TestMethod] public void SystemOfUnitsIdTest() => isReadOnly(obj.Data.SystemOfUnitsId);
        [TestMethod] public void SystemOfUnitsTest() {
            var d = random<SystemOfUnitsData>();
            d.Id = obj.SystemOfUnitsId;
            add<ISystemsOfUnitsRepo, SystemOfUnits>(new SystemOfUnits(d));
            allPropertiesAreEqual(d, obj.SystemOfUnits.Data);
        }
        [TestMethod] public void UnitIdTest() => isReadOnly(obj.Data.UnitId);
        [TestMethod] public void UnitTest() {
            var d = random<UnitData>();
            d.UnitType = UnitType.Factored;
            d.Id = obj.UnitId;
            add<IUnitsRepo, Unit>(new FactoredUnit(d));
            allPropertiesAreEqual(d, obj.Unit.Data);
        }
    }
}