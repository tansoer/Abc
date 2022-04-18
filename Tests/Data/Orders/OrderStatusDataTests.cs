using Abc.Data.Common;
using Abc.Data.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders {
    [TestClass]
    public class OrderStatusDataTests: SealedTests<OrderStatusData, EntityBaseData> {
        protected override OrderStatusData createObject()
            => random<OrderStatusData>();
        [TestMethod] public void OrderStatusTest() => isProperty<OrderLifecycleStatus>();
        [TestMethod] public void OrderIdTest() => isNullable<string>();
        [TestMethod] public void OrderEventIdTest() => isNullable<string>();
    }
}
