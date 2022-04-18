using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Lines {

    [TestClass]
    public class ReceiptLineTests :SealedTests<ReceiptLine, DeliveryOrderLine> {
        protected override ReceiptLine createObject() {
            var d = random<OrderLineData>();
            d.OrderLineType = OrderLineType.ReceiptLine;
            return OrderLineFactory.Create(d) as ReceiptLine;
        }

        [TestMethod] public void IsAssessedTest() => isReadOnly(obj.Data.IsAssessed);
        [TestMethod] public void NumberReceivedTest() => isReadOnly(obj.NumberOfProducts);
    }

}