using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Abc.Tests.Domain.Orders.Lines {
    public sealed class MockLinesManager :MockOrderManager, IOrderLinesManager {

        public MockLinesManager(IOrder o) : base(o) { }
        public IReadOnlyList<OrderLine> ProductLines => registerMethod<IReadOnlyList<OrderLine>>();
        public IReadOnlyList<ChargeLine> ChargeLines => registerMethod<IReadOnlyList<ChargeLine>>();
        public bool Amend(AmendOrderLineEvent e) => registerMethod<bool>(e);
        public bool IsCancellable() => registerMethod<bool>();
        public bool IsClosable() => registerMethod<bool>();
        public bool IsProductLine(IOrderLine l) => registerMethod<bool>(l);
    }
    [TestClass]
    public class OrderLinesManagerTests :SealedTests<OrderLinesManager, OrderManager> {
        private IOrder order;

        [TestInitialize]
        public override void TestInitialize() {
            order = OrderFactory.Create(random<OrderData>());
            base.TestInitialize();
        }
        protected override OrderLinesManager createObject() => new(order);
    }
}