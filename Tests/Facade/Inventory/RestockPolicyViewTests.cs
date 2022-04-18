using Abc.Facade.Common;
using Abc.Facade.Inventory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Inventory {
    [TestClass]
    public class RestockPolicyViewTests :SealedTests<RestockPolicyView, EntityBaseView> {
        [TestMethod] public void RestockRuleSetIdTest() => isNullableProperty<string>("Restock Rule Set");
        [TestMethod] public void RestockRuleContextIdTest() => isNullableProperty<string>("Restock Rule Context");
    }
}
