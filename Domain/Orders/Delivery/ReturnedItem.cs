using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Products;

namespace Abc.Domain.Orders.Delivery {
    public interface IReturnedItemsRepo :IRepo<ReturnedItem> {
    }
    public sealed class ReturnedItem :Entity<ReturnedItemData> {
        public ReturnedItem() : this(null) { }
        public ReturnedItem(ReturnedItemData d) : base(d) { }
        public string OrderEventId => get(Data?.OrderEventId);
        public string ProductId => get(Data?.ProductId);
        public IProduct Product => item<IProductsRepo, IProduct>(ProductId);
    }
}
