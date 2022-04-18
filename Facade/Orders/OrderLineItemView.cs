using Abc.Data.Orders;
using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders
{
    public sealed class OrderLineItemView : EntityBaseView
    {
        [DisplayName("Order line")] public string OrderLineId { get; set; }
        [DisplayName("Product")] public string ProductId { get; set; }
        [DisplayName("Order line type")] public OrderLineType OrderLineType { get; set; }
    }
}
