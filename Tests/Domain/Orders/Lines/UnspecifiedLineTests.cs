using Abc.Data.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Lines {

    [TestClass]
    public class UnspecifiedLineTests :SealedTests<UnspecifiedLine, BaseOrderLine> {
        protected override UnspecifiedLine createObject() {
            var d = random<OrderLineData>();
            d.OrderLineType = OrderLineType.Unspecified;
            return OrderLineFactory.Create(d) as UnspecifiedLine;
        }
    }
}