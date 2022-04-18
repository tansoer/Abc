using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Rules;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Currencies {

    [TestClass]
    public class ExchangeRuleTests :SealedTests<ExchangeRule, Entity<ExchangeRuleData>> {
        protected override ExchangeRule createObject() => new (GetRandom.ObjectOf<ExchangeRuleData>());
        [TestMethod] public void RateTypeIdTest() => isReadOnly(obj.Data.RateTypeId);
        [TestMethod] public void RuleSetIdTest() => isReadOnly(obj.Data.RuleSetId);
        [TestMethod]
        public async Task RateTypeTest() =>
            await testItemAsync<RateTypeData, RateType, IRateTypesRepo>(
                obj.RateTypeId, () => obj.RateType.Data, d => new RateType(d));
        [TestMethod]
        public async Task RuleSetTest() =>
            await testItemAsync<RuleSetData, IRuleSet, IRuleSetsRepo>(
                obj.RuleSetId, () => obj.RuleSet.Data, d => new RuleSet(d));
    }
}
