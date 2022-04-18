using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass] public class OrderLineItemDataTests :SealedTests<OrderLineItemData, EntityBaseData> {

        [TestMethod] public void OrderLineIdTest() => isNullable<string>();
        [TestMethod] public void ProductIdTest() => isNullable<string>();
        [TestMethod] public void OrderLineTypeTest() => isProperty<OrderLineType>();
    }
}