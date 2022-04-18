using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Domain.Orders {

    [TestClass]
    public class OrderManagerTests :AbstractTests<OrderManager, Archetype> {
        private class testClass :OrderManager {
            public testClass() : this(null) { }
            public testClass(IOrder o) : base(o) { }
        }
        protected override OrderManager createObject()
            => new testClass(OrderFactory.Create(random<OrderData>()));

    }
}
