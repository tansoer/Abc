using Abc.Data.Orders;
using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders {
    public sealed class OrderStatusView : EntityBaseView {
        [DisplayName("Order")] public string OrderId { get; set; }
        [DisplayName("Order event")] public string OrderEventId { get; set; }
        [DisplayName("Order status")] public OrderLifecycleStatus OrderStatus { get; set; }
    }
}
