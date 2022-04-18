using Abc.Data.Orders;
using Abc.Facade.Common;
using System;
using System.ComponentModel;

namespace Abc.Facade.Orders {
    public sealed class OrderLineView : EntityBaseView {
        [DisplayName("Order")] public string OrderId { get; set; }
        [DisplayName("Order event")] public string OrderEventId { get; set; }
        [DisplayName("Product type")] public string ProductTypeId { get; set; }
        [DisplayName("Product")] public string ProductId { get; set; }
        [DisplayName("Is assessed")] public bool IsAssessed { get; set; }
        [DisplayName("Order line type")] public OrderLineType OrderLineType { get; set; }
        [DisplayName("Number of products")] public ushort NumberOfProducts { get; set; }
        [DisplayName("Expected delivery")] public DateTime? ExpectedDelivery { get; set; }
        [DisplayName("Amount")] public decimal Amount { get; set; }
        [DisplayName("Currency")] public string CurrencyId { get; set; }
        [DisplayName("Order line")] public string OrderLineId { get; set; }
        [DisplayName("Sales tax policy")] public string SalesTaxPolicyId { get; set; }
    }
}
