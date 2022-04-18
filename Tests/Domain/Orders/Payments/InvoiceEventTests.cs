using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Payments {

    [TestClass]
    public sealed class InvoiceEventTests :SealedTests<InvoiceEvent, PaymentEvent> {

        protected override InvoiceEvent createObject() {
            var d = random<OrderEventData>();
            d.OrderEventType = OrderEventType.InvoiceEvent;
            return OrderEventFactory.Create(d) as InvoiceEvent;
        }
        [TestMethod] public void InvoiceIdTest() => isReadOnly(obj.Data.InvoiceId, true);
        [TestMethod] public async Task InvoiceTest() 
            => await testItemAsync<InvoiceData, Invoice, IInvoicesRepo>(
                obj.invoiceId, ()=> obj.Invoice.Data, d => new Invoice(d));
    }
}