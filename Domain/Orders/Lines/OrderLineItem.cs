using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Products;

namespace Abc.Domain.Orders.Lines {
    public interface IOrderLineItem :IEntity<OrderLineItemData> { }

    public interface IOrderLineItemsRepo :IRepo<IOrderLineItem> { }

    public abstract class OrderLineItem :Entity<OrderLineItemData>, IOrderLineItem {
        protected OrderLineItem() : this(null) { }
        protected OrderLineItem(OrderLineItemData d) : base(d) { }
        public string OrderLineId => get(Data?.OrderLineId);
        public string ProductId => get(Data?.ProductId);
        public IOrderLine OrderLine => item<IOrderLinesRepo, IOrderLine>(OrderLineId);
        public IProduct Product => item<IProductsRepo, IProduct>(Data?.ProductId);
    }
}