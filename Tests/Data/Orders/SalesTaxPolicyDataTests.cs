using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class SalesTaxPolicyDataTests :SealedTests<SalesTaxPolicyData, EntityBaseData> {
        [TestMethod] public void OrderManagerIdTest() => isNullable<string>();
        [TestMethod] public void TaxationRateTest() => isProperty<decimal>();
        [TestMethod] public void TaxationTypeIdTest() => isProperty<string>();
    }
}