using Abc.Data.Orders;

namespace Abc.Domain.Orders.Lines {
    public class OrderLineFactory {
        public static IOrderLine Create(OrderLineData d) =>
             d?.OrderLineType switch {
                OrderLineType.ProductLine => new OrderLine(d),
                OrderLineType.ChargeLine => new ChargeLine(d),
                OrderLineType.TaxLine => new TaxLine(d),
                OrderLineType.DispatchLine => new DispatchLine(d),
                OrderLineType.ReceiptLine => new ReceiptLine(d),
                _ => new UnspecifiedLine(d)
            };
    }
}