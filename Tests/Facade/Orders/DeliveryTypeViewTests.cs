using Abc.Data.Orders;
using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class DeliveryTypeViewTests :SealedTests<DeliveryTypeView, EntityTypeView> {
    }
    [TestClass]
    public class OrderLineItemViewTests :SealedTests<OrderLineItemView, EntityBaseView> {
        [TestMethod] public void OrderLineIdTest() => isNullableProperty<string>("Order line");
        [TestMethod] public void ProductIdTest() => isNullableProperty<string>("Product");
        [TestMethod] public void OrderLineTypeTest() => isProperty<OrderLineType>("Order line type");
    }
}