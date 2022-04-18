using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders
{
    public sealed class ReturnedItemView : EntityBaseView
    {
        [DisplayName("Order event")] public string OrderEventId { get; set; }
        [DisplayName("Product")] public string ProductId { get; set; }
    }
}
