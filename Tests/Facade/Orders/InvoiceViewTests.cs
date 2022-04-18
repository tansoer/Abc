using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class InvoiceViewTests :SealedTests<InvoiceView, EntityBaseView> {
        [TestMethod] public void DocumentTest() => isNullableProperty<string>("Document");
        [TestMethod] public void OrderIdTest() => isNullableProperty<string>("Order");
    }
}