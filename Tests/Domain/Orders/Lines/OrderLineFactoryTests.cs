using Abc.Aids.Random;
using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Domain.Orders.Lines {

    [TestClass]
    public class OrderLineFactoryTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(OrderLineFactory);

        [DataTestMethod]
        [DataRow(OrderLineType.ProductLine, typeof(OrderLine))]
        [DataRow(OrderLineType.ChargeLine, typeof(ChargeLine))]
        [DataRow(OrderLineType.TaxLine, typeof(TaxLine))]
        [DataRow(OrderLineType.DispatchLine, typeof(DispatchLine))]
        [DataRow(OrderLineType.ReceiptLine, typeof(ReceiptLine))]
        public void CreateTest(OrderLineType type, Type t) {
            var d = GetRandom.ObjectOf<OrderLineData>();
            d.OrderLineType = type;
            var o = OrderLineFactory.Create(d);
            Assert.IsInstanceOfType(o, t);
            allPropertiesAreEqual(d, o.Data);
        }
    }
}