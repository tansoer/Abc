using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class OrderEventDataTests :SealedTests<OrderEventData, EntityBaseData> {
        [TestMethod] public void OrderEventTypeTest() => isProperty<OrderEventType>();
        [TestMethod] public void OrderIdTest() => isNullable<string>();
        [TestMethod] public void IsProcessedTest() => isProperty<bool>();
        [TestMethod] public void AuthorizedPartySignatureIdTest() => isNullable<string>();
        [TestMethod] public void OldOrderLineIdTest() => isNullable<string>();
        [TestMethod] public void OrderLineIdTest() => isNullable<string>();
        [TestMethod] public void OldPartySummaryIdTest() => isNullable<string>();
        [TestMethod] public void PartySummaryIdTest() => isNullable<string>();
        [TestMethod] public void OldTermsAndConditionsIdTest() => isNullable<string>();
        [TestMethod] public void TermsAndConditionsIdTest() => isNullable<string>();
        [TestMethod] public void PaymentIdTest() => isNullable<string>();
        [TestMethod] public void InvoiceIdTest() => isNullable<string>();
        [TestMethod] public void ProductDeliveryIdTest() => isNullable<string>();
    }
}