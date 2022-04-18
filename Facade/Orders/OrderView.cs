using Abc.Data.Orders;
using Abc.Facade.Common;
using System;
using System.ComponentModel;

namespace Abc.Facade.Orders {
    public sealed class OrderView : EntityBaseView {
        [DisplayName("Order manager")] public string OrderManagerId { get; set; }
        [DisplayName("Order type")] public OrderType OrderType { get; set; }
        [DisplayName("Date sent")] public DateTime? DateSent { get; set; }
        [DisplayName("Purchase order received")] public DateTime? DatePurchaseOrderReceived { get; set; }
        [DisplayName("Sales channel")] public string SalesChannelId { get; set; }
        [DisplayName("Discount rule context")] public string DiscountRuleContextId { get; set; }
        [DisplayName("Purchase order")] public string PurchaseOrderId { get; set; }
        public OrderLifecycleStatus Status { get; set; }
        [DisplayName("Date created")] public new DateTime? ValidFrom { get; set; }
    }
}
