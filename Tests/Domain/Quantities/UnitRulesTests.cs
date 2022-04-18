using Abc.Aids.Random;
using Abc.Data.Quantities;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class UnitRulesTests : SealedTests<UnitRules, UnitAttribute<UnitRulesData>> {
        protected override UnitRules createObject() 
            => new UnitRules(GetRandom.ObjectOf<UnitRulesData>());
        [TestMethod] public void SystemOfUnitsIdTest() => isReadOnly(obj.Data.SystemOfUnitsId);
        
        [TestMethod] public void FromBaseUnitRuleIdTest() => isReadOnly(obj.Data.FromBaseUnitRuleId);

        [TestMethod] public void ToBaseUnitRuleIdTest() => isReadOnly(obj.Data.ToBaseUnitRuleId);


        [TestMethod]
        public void SystemOfUnitsTest() {
            var d = GetRandom.ObjectOf<SystemOfUnitsData>();
            d.Id = obj.SystemOfUnitsId;
            GetRepo.Instance<ISystemsOfUnitsRepo>().Add(new SystemOfUnits(d));
            allPropertiesAreEqual(d, obj.SystemOfUnits.Data);
        }
        [TestMethod] public void UnitIdTest() => isReadOnly(obj.Data.UnitId);

        [TestMethod]
        public void UnitTest() {
            var d = GetRandom.ObjectOf<UnitData>();
            d.UnitType = UnitType.Factored;
            d.Id = obj.UnitId;
            GetRepo.Instance<IUnitsRepo>().Add(new FactoredUnit(d));

            allPropertiesAreEqual(d, obj.Unit.Data);
        }

        [TestMethod]
        public void FromBaseUnitRuleTest() {
            var r = GetRepo.Instance<IRulesRepo>();
            var d = GetRandom.ObjectOf<RuleData>();
            d.Id = obj.FromBaseUnitRuleId;
            d.RuleKind = RuleKind.Rule;
            r.Add(new Rule(d));
            allPropertiesAreEqual(d, obj.FromBaseUnitRule.Data);
        }

        [TestMethod]
        public void ToBaseUnitRuleTest() {
            var r = GetRepo.Instance<IRulesRepo>();
            var d = GetRandom.ObjectOf<RuleData>();
            d.Id = obj.ToBaseUnitRuleId;
            d.RuleKind = RuleKind.Rule;
            r.Add(new Rule(d));
            allPropertiesAreEqual(d, obj.ToBaseUnitRule.Data);
        }

    }

}