using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class InvoiceDataTests :SealedTests<InvoiceData, EntityBaseData> {
        [TestMethod] public void DocumentTest() => isNullable<string>();
        [TestMethod] public void OrderIdTest() => isNullable<string>();
    }
}
