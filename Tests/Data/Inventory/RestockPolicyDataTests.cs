using Abc.Data.Common;
using Abc.Data.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Inventory {
    [TestClass] public class RestockPolicyDataTests :SealedTests<RestockPolicyData, EntityBaseData>{
        [TestMethod] public void RestockRuleSetIdTest() => isNullable<string>();
        [TestMethod] public void RestockRuleContextIdTest() => isNullable<string>();
    }
}
