using Abc.Data.Orders;

namespace Abc.Domain.Orders.Lines {
    public sealed class UnspecifiedLine :BaseOrderLine {
        public UnspecifiedLine() : this(null) { }
        public UnspecifiedLine(OrderLineData d) : base(d) { }
    }
}