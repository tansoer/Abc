using Abc.Data.Orders;
using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass] public class OrderEventViewTests : SealedTests<OrderEventView, EntityBaseView> {
        [TestMethod] public void OrderEventTypeTest() => isProperty<OrderEventType>("Order event type");
        [TestMethod] public void OrderIdTest() => isNullableProperty<string>("Order");
        [TestMethod] public void PaymentIdTest() => isNullableProperty<string>("Payment");
        [TestMethod] public void IsProcessedTest() => isProperty<bool>("Is processed");
        [TestMethod] public void AuthorizedPartySignatureIdTest() => isNullableProperty<string>("Authorized party signature");
        [TestMethod] public void OldOrderLineIdTest() => isNullableProperty<string>("Old order line");
        [TestMethod] public void OrderLineIdTest() => isNullableProperty<string>("Order line");
        [TestMethod] public void OldPartySummaryIdTest() => isNullableProperty<string>("Old party summary");
        [TestMethod] public void PartySummaryIdTest() => isNullableProperty<string>("Party summary");
        [TestMethod] public void OldTermsAndConditionsIdTest() => isNullableProperty<string>("Old terms and conditions");
        [TestMethod] public void TermsAndConditionsIdTest() => isNullableProperty<string>("Terms and conditions");
        [TestMethod] public void ProductDeliveryIdTest() => isNullableProperty<string>("Product delivery");
        [TestMethod] public void InvoiceIdTest() => isNullableProperty<string>("Invoice");
    }
}