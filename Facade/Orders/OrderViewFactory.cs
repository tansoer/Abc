using Abc.Aids.Methods;
using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Common;

namespace Abc.Facade.Orders
{
    public sealed class OrderViewFactory :AbstractViewFactory<OrderData, IOrder, OrderView> {
        protected internal override IOrder toObject(OrderData d) => OrderFactory.Create(d);
        public override IOrder Create(OrderView v) {
            var d = new OrderData();
            if (!(v is null)) Copy.Members(v, d);
            d.DateSentReceived = (v.OrderType == OrderType.SalesOrder) ? v.DateSent: v.DatePurchaseOrderReceived;
            return toObject(d);
        }
        public override OrderView Create(IOrder o) {
            var v = new OrderView();
            if (!(o is null)) Copy.Members(o.Data, v);
            v.DateSent = o.Data.DateSentReceived;
            v.DatePurchaseOrderReceived = o.Data.DateSentReceived;
            return v;
        }
    }
}
