using Abc.Data.Orders;
using Abc.Domain.Orders.Payments;
using Abc.Infra.Common;

namespace Abc.Infra.Orders {
    public sealed class InvoicesRepo :PagedRepo<Invoice, InvoiceData>,
        IInvoicesRepo {

        public InvoicesRepo(OrderDb c = null) : base(c, c?.Invoices) { }

        protected internal override Invoice toDomainObject(InvoiceData d) => new(d);
    }
}