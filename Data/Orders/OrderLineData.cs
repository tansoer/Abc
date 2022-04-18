using Abc.Data.Common;
using System;

namespace Abc.Data.Orders {
    public sealed class OrderLineData :EntityBaseData {
        public string OrderId { get; set; }
        public string OrderEventId { get; set; }
        public string ProductTypeId { get; set; }
        public string ProductId { get; set; }
        public bool IsAssessed { get; set; }
        public OrderLineType OrderLineType {get; set; }
        public ushort NumberOfProducts { get; set; }
        public DateTime? ExpectedDelivery { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyId { get; set; }
        public string OrderLineId { get; set; }
        public string SalesTaxPolicyId { get; set; }
    }
}
