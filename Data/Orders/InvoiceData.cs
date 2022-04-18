using Abc.Data.Common;

namespace Abc.Data.Orders {
    public sealed class InvoiceData: EntityBaseData {
        public string Document {  get; set; }
        public string OrderId { get; set; }
    }
}
