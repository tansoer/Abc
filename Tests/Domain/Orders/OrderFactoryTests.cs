using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Domain.Orders {

    [TestClass]
    public class OrderFactoryTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(OrderFactory);

        [DataTestMethod]
        [DataRow(OrderType.PurchaseOrder, typeof(PurchaseOrder))]
        [DataRow(OrderType.SalesOrder, typeof(SalesOrder))]
        public void CreateTest(OrderType type, Type t) {
            var d = GetRandom.ObjectOf<OrderData>();
            d.OrderType = type;
            var o = OrderFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}