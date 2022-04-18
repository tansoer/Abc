using Abc.Aids.Methods;
using Abc.Data.Inventory;
using Abc.Data.Products;
using Abc.Domain.Inventory;
using Abc.Domain.Products;
using Abc.Facade.Inventory;
using Abc.Pages.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Abc.Pages.Inventory {
    public sealed class InventoryEntriesPage :ViewsPage<InventoryEntriesPage, IInventoryEntriesRepo, 
        InventoryEntry, InventoryEntryView, InventoryEntryData, ProductKind> {
        protected override string title => InventoryTitles.InventoryEntries;
        public InventoryEntriesPage(IInventoryEntriesRepo r) : base(r) {}
        private IEnumerable<SelectListItem> inventories;
        public IEnumerable<SelectListItem> Inventories => inventories ??= selectListByName<IInventoriesRepo, Domain.Inventory.Inventory, InventoryData>();
        private IEnumerable<SelectListItem> productTypes;
        public IEnumerable<SelectListItem> ProductTypes => productTypes ??= selectListByName<IProductTypesRepo, IProductType, ProductTypeData>();
        private IEnumerable<SelectListItem> capacityManagers;
        public IEnumerable<SelectListItem> CapacityManagers => capacityManagers ??= selectListByName<ICapacityManagersRepo, CapacityManager, CapacityManagerData>();
        private IEnumerable<SelectListItem> restockPolicies;
        public IEnumerable<SelectListItem> RestockPolicies => restockPolicies ??= selectListByName<IRestockPoliciesRepo, RestockPolicy, RestockPolicyData>();
        protected override void tableColumns() {
            tableColumn(x => Item.InventoryId, () => InventoryName(Item.InventoryId));
            tableColumn(x => Item.InventoryEntryType);
            tableColumn(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            tableColumn(x => Item.ValidFrom);
            tableColumn(x => Item.ValidTo);
        }
        protected override void valueViewers() {
            valueViewer(x => Item.Id);
            valueViewer(x => Item.Name);
            valueViewer(x => Item.Code);
            valueViewer(x => Item.InventoryEntryType);
            valueViewer(x => Item.InventoryId, () => InventoryName(Item.InventoryId));
            valueViewer(x => Item.ProductTypeId, () => ProductTypeName(Item.ProductTypeId));
            if (Item.InventoryEntryType is ProductKind.Service) {
                valueViewer(x => Item.CapacityManagerId, () => CapacityManagerName(Item.CapacityManagerId));
            } else {
                valueViewer(x => Item.RestockPolicyId, () => RestockPolicyName(Item.RestockPolicyId));
            }
            valueViewer(x => Item.Details);
            valueViewer(x => Item.ValidFrom);
            valueViewer(x => Item.ValidTo);
        }
        protected override void valueEditors() {
            valueEditor(x => Item.InventoryId, () => Inventories);
            valueEditor(x => Item.ProductTypeId, () => ProductTypes);
            valueEditor(x => Item.Name);
            valueEditor(x => Item.Code);

            if (Item.InventoryEntryType is ProductKind.Service) {
                valueEditor(x => Item.CapacityManagerId, () => CapacityManagers);
            } else {
                valueEditor(x => Item.RestockPolicyId, () => RestockPolicies);
            }

            valueEditor(x => Item.Details);
            valueEditor(x => Item.ValidFrom);
            valueEditor(x => Item.ValidTo);
        }
        protected internal override string pageUrl => InventoryUrls.InventoryEntries;
        protected internal override InventoryEntry toObject(InventoryEntryView v)
            => new InventoryEntryViewFactory().Create(v);
        protected internal override InventoryEntryView toView(InventoryEntry o)
            => new InventoryEntryViewFactory().Create(o);
        protected override void doBeforeOnGetCreate(string fixedFilter, string fixedValue, int? switchOfCreate) {
            var entryType = Safe.Run(() => (ProductKind)(switchOfCreate ?? 1000), ProductKind.Unspecified);
            Item = new() {InventoryEntryType = entryType };
            productTypes = selectListByName<IProductTypesRepo, IProductType, ProductTypeData>(x=> x.ProductKind == entryType);
        }
        protected override void doAfterOnGetEdit()
            => productTypes = selectListByName<IProductTypesRepo, IProductType, ProductTypeData>(x => x.ProductKind == Item.InventoryEntryType);
        public string InventoryName(string id) => itemName(Inventories, id);
        public string ProductTypeName(string id) => itemName(ProductTypes, id);
        public string CapacityManagerName(string id) => itemName(CapacityManagers, id);
        public string RestockPolicyName(string id) => itemName(RestockPolicies, id);
    }
}
