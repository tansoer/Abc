using Abc.Data.Inventory;
using Abc.Domain.Inventory;
using Abc.Facade.Inventory;
using Abc.Pages.Common;

namespace Abc.Pages.Inventory {
    public sealed class InventoriesPage : ViewPage<InventoriesPage, IInventoriesRepo, 
        Domain.Inventory.Inventory, InventoryView, InventoryData> {
        protected override string title => InventoryTitles.Inventories;
        public InventoriesPage(IInventoriesRepo r) : base(r) { }
        protected override void tableColumns() {
            tableColumn(x => Item.Name);
            tableColumn(x => Item.Code);
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);
            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => InventoryUrls.Inventories;
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate)
            => Item = new();
        protected internal override Domain.Inventory.Inventory toObject(InventoryView v)
            => new InventoryViewFactory().Create(v);
        protected internal override InventoryView toView(Domain.Inventory.Inventory o)
            => new InventoryViewFactory().Create(o);
    }
}