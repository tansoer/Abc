using Abc.Data.Orders;
using Abc.Domain.Orders;
using Abc.Facade.Orders;
using Abc.Pages.Common;

namespace Abc.Pages.Orders {

    public sealed class OrdersPage: ViewsPage<OrdersPage, IOrdersRepo, IOrder, OrderView, OrderData, OrderType> {
        protected override string title => OrderTitles.Orders;
        public OrdersPage(IOrdersRepo r): base(r) { }
        protected internal override string pageUrl => OrderUrls.Orders;
        protected internal override IOrder toObject(OrderView v) =>new OrderViewFactory().Create(v);
        protected internal override OrderView toView(IOrder o) => new OrderViewFactory().Create(o);
        protected override void tableColumns() { }
    }
}
