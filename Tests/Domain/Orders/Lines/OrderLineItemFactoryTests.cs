using Abc.Domain.Orders.Lines;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Lines {
    [TestClass]
    public class OrderLineItemFactoryTests :TestsBase {
        [TestInitialize]
        public void TestInitialize() => type = typeof(OrderLineItemFactory);
    }
}
