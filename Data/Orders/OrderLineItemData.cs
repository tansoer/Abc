using Abc.Data.Common;

namespace Abc.Data.Orders {
    public sealed class OrderLineItemData :EntityBaseData {
        public string OrderLineId { get; set; }
        public string ProductId { get; set; }
        public OrderLineType OrderLineType { get; set; }
    }
}
