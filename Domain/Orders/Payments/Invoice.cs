using Abc.Data.Orders;
using Abc.Domain.Common;

namespace Abc.Domain.Orders.Payments {
    
    public interface IInvoicesRepo :IRepo<Invoice> { }

    public sealed class Invoice: Entity<InvoiceData> {
        public Invoice() : this(null) { }
        public Invoice(InvoiceData d) : base(d) { }
        public string Document => get(Data?.Document);

        internal string orderId => get(Data?.OrderId);
        public IOrder Order => item<IOrdersRepo, IOrder>(orderId);
    }
}
