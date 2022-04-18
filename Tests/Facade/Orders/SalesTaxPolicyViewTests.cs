using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class SalesTaxPolicyViewTests :SealedTests<SalesTaxPolicyView, EntityBaseView> {
        [TestMethod] public void OrderManagerIdTest() => isNullableProperty<string>("Order manager");
        [TestMethod] public void TaxationRateTest() => isProperty<decimal>("Taxation rate");
        [TestMethod] public void TaxationTypeIdTest() => isNullableProperty<string>("Taxation type");
    }
}