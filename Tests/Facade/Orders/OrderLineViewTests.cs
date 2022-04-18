using Abc.Data.Orders;
using Abc.Facade.Common;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Facade.Orders {
    [TestClass]
    public class OrderLineViewTests :SealedTests<OrderLineView, EntityBaseView> {
        [TestMethod] public void OrderIdTest() => isNullableProperty<string>("Order");
        [TestMethod] public void OrderEventIdTest() => isNullableProperty<string>("Order event");
        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product type");
        [TestMethod] public void ProductIdTest() => isNullableProperty<string>("Product");
        [TestMethod] public void OrderLineTypeTest() => isProperty<OrderLineType>("Order line type");
        [TestMethod] public void NumberOfProductsTest() => isProperty<ushort>("Number of products");
        [TestMethod] public void ExpectedDeliveryTest() => isNullableProperty<DateTime?>("Expected delivery");
        [TestMethod] public void AmountTest() => isProperty<decimal>("Amount");
        [TestMethod] public void CurrencyIdTest() => isNullableProperty<string>("Currency");
        [TestMethod] public void OrderLineIdTest() => isNullableProperty<string>("Order line");
        [TestMethod] public void SalesTaxPolicyIdTest() => isNullableProperty<string>("Sales tax policy");
        [TestMethod] public void IsAssessedTest() => isProperty<bool>("Is assessed");
    }
}