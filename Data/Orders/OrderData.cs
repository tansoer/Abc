using Abc.Data.Common;
using System;

namespace Abc.Data.Orders {
    public sealed class OrderData : EntityBaseData { 
        public string OrderManagerId { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime? DateSentReceived { get; set; }
        public string SalesChannelId { get; set; }
        public string DiscountRuleContextId { get; set; }
        public string PurchaseOrderId { get; set; }
        public OrderLifecycleStatus Status { get; set; }
    }
}
