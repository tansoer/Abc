using Abc.Data.Common;

namespace Abc.Data.Orders {
    public sealed class ReturnedItemData: EntityBaseData {
        public string OrderEventId { get; set; }
        public string ProductId { get; set; }
    }
}
