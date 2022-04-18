using Abc.Data.Orders;

namespace Abc.Domain.Orders {
    public sealed class UnspecifiedOrder :Order {
        public UnspecifiedOrder() : this(null) { }
        public UnspecifiedOrder(OrderData d) : base(d) { }
    }
}
