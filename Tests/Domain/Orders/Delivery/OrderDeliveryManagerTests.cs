using Abc.Domain.Orders.Delivery;
using Abc.Domain.Orders.Payments;
using Abc.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abc.Data.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abc.Facade.Orders;

namespace Abc.Tests.Domain.Orders.Delivery {
    public sealed class MockDeliveryManager :MockOrderManager, IOrderDeliveryManager {

        public MockDeliveryManager(IOrder o) : base(o) { }

        public bool AcceptReceipt(IReceiptEvent e) => registerMethod<bool>(e);
        public bool DispatchItems(IDispatchEvent e) => registerMethod<bool>(e);
        public bool ReceiptReturned(IReceiptEvent e) => registerMethod<bool>(e);
        public bool ReturnItems(IDispatchEvent e) => registerMethod<bool>(e);
    }

        [TestClass]
        public class OrderDeliveryManagerTests :SealedTests<OrderDeliveryManager, OrderManager> {
            private IOrder order;

            [TestInitialize]
            public override void TestInitialize() {
                order = OrderFactory.Create(random<OrderData>());
                base.TestInitialize();
            }
            protected override OrderDeliveryManager createObject() => new(order);
        }
    }
