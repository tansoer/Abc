using Abc.Data.Orders;

namespace Abc.Domain.Orders.Lines {
    public sealed class ReceiptLine :DeliveryOrderLine {
        public ReceiptLine() : this(null) { }
        public ReceiptLine(OrderLineData d) : base(d) { }
        public bool IsAssessed => get(data?.IsAssessed);
        public int NumberReceived => NumberOfProducts;
    }

}