using Abc.Facade.Common;
using System.ComponentModel;

namespace Abc.Facade.Inventory {
    public abstract class InventoryItemView :EntityBaseView{
        [DisplayName("Inventory")]
        public string InventoryId { get; set; }
    }
}
