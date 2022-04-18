using Abc.Data.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Facade.Common;

namespace Abc.Facade.Orders {
    public sealed class InvoiceViewFactory
        :AbstractViewFactory<InvoiceData, Invoice, InvoiceView> {
        protected internal override Invoice toObject(InvoiceData d) =>
            new(d);

    }
}