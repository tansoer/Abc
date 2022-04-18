using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Payments {
    [TestClass]
    public class InvoiceTests :SealedTests<Invoice, Entity<InvoiceData>> {
        protected override Invoice createObject() => new (random<InvoiceData>());
        [TestMethod] public void DocumentTest() => isReadOnly(obj.Data.Document);
        [TestMethod] public void OrderIdTest() => isReadOnly(obj.Data.OrderId, true);
        [TestMethod] public async Task OrderTest() => 
            await testItemAsync<OrderData, IOrder, IOrdersRepo>(
                obj.orderId, () => obj.Order.Data, d => OrderFactory.Create(d)) ;
    }
}