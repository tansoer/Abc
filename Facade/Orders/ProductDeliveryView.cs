using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Orders
{
    public sealed class ProductDeliveryView : EntityBaseView
    {
        [DisplayName("Delivery type")]public string DeliveryTypeId { get; set; }
    }
}
