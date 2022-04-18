using System;
using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class OrderLineDataTests :SealedTests<OrderLineData, EntityBaseData> {
        [TestMethod] public void OrderIdTest() => isNullable<string>();
        [TestMethod] public void OrderEventIdTest() => isNullable<string>();
        [TestMethod] public void ProductTypeIdTest() => isNullable<string>();
        [TestMethod] public void ProductIdTest() => isNullable<string>();
        [TestMethod] public void OrderLineTypeTest() => isProperty<OrderLineType>();
        [TestMethod] public void NumberOfProductsTest() => isProperty<ushort>();
        [TestMethod] public void ExpectedDeliveryTest() => isNullable<DateTime?>();
        [TestMethod] public void AmountTest() => isProperty<decimal>();
        [TestMethod] public void CurrencyIdTest() => isNullable<string>();
        [TestMethod] public void OrderLineIdTest() => isNullable<string>();
        [TestMethod] public void SalesTaxPolicyIdTest() => isNullable<string>();
        [TestMethod] public void IsAssessedTest() => isProperty<bool>();
    }
}