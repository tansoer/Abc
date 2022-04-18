using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Lines {

    [TestClass]
    public class DispatchLineTests :SealedTests<DispatchLine, DeliveryOrderLine> {
        protected override DispatchLine createObject() {
            var d = random<OrderLineData>();
            d.OrderLineType = OrderLineType.DispatchLine;
            return OrderLineFactory.Create(d) as DispatchLine;
        }
        [TestMethod] public void NumberDispatchedTest() => isReadOnly(obj.NumberOfProducts); 
    }
}