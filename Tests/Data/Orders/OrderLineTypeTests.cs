using Abc.Data.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Orders
{
    [TestClass]
    public class OrderLineTypeTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(OrderLineType);

        [TestMethod] public void UnspecifiedTest() => Assert.AreEqual(0, (int)OrderLineType.Unspecified);
        [TestMethod] public void ProductLineTest() => Assert.AreEqual(1, (int)OrderLineType.ProductLine);
        [TestMethod] public void ChargeLineTest() => Assert.AreEqual(2, (int)OrderLineType.ChargeLine);
        [TestMethod] public void TaxLineTest() => Assert.AreEqual(3, (int)OrderLineType.TaxLine);
        [TestMethod] public void DispatchLineTest() => Assert.AreEqual(4, (int)OrderLineType.DispatchLine);
        [TestMethod] public void ReceiptLineTest() => Assert.AreEqual(5, (int)OrderLineType.ReceiptLine);
    }
}