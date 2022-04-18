using Abc.Data.Common;
using Abc.Data.Currencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Currencies {

    [TestClass]
    public class ExchangeRuleDataTests : SealedTests<ExchangeRuleData, EntityBaseData> {

        [TestMethod] public void RateTypeIdTest() => isNullable<string>();

        [TestMethod] public void RuleSetIdTest() => isNullable<string>();
    }
}