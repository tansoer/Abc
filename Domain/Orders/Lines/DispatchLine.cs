using Abc.Data.Orders;

namespace Abc.Domain.Orders.Lines {
    public sealed class DispatchLine :DeliveryOrderLine {
        public DispatchLine() : this(null) { }
        public DispatchLine(OrderLineData d) : base(d) { }
        public int NumberDispatched => NumberOfProducts;
    }

}