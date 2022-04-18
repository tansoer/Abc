using Abc.Data.Orders;
using Abc.Domain.Currencies;

namespace Abc.Domain.Orders.Lines {
    public sealed class ChargeLine :TaxableLine {
        public ChargeLine() : this(null) { }
        public ChargeLine(OrderLineData d) : base(d) { }
        public Money Amount => new(amount, currencyId);
        public OrderLine ProductLine => item<IOrderLinesRepo, IOrderLine>(OrderLineId) as OrderLine;
    }
}