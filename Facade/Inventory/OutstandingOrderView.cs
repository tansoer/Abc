using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Inventory {
    public sealed class OutstandingOrderView :EntityBaseView {
        [DisplayName("Purchase Order")]
        public string PurchaseOrderId { get; set; }
        [DisplayName("Inventory Entry")]
        public string InventoryEntryId { get; set; }
    }
}
