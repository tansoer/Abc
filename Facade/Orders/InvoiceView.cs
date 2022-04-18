using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders
{
    public sealed class InvoiceView :EntityBaseView {
        public string Document { get; set; }
        [DisplayName("Order")] public string OrderId { get; set; }
    }
}
