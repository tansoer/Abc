using Abc.Data.Inventory;
using Abc.Domain.Orders;
using System.Collections.Generic;

namespace Abc.Domain.Inventory {
    public sealed class ProductInventoryEntry :InventoryEntry {
        private static string entryId => nameOf<OutstandingOrderData>(x => x.InventoryEntryId);

        public ProductInventoryEntry() : this(null) { }
        public ProductInventoryEntry(InventoryEntryData d) : base(d) { }
        public string RestockPolicyId => get(Data?.RestockPolicyId);
        public RestockPolicy RestockPolicy 
            => item<IRestockPoliciesRepo, RestockPolicy>(RestockPolicyId);
        internal IReadOnlyList<OutstandingOrder> outstandingOrders
            => list<IOutstandingOrdersRepo, OutstandingOrder>(entryId, Id);
        public IReadOnlyList<PurchaseOrder> OutstandingOrders
            => list(outstandingOrders, x => x.PurchaseOrder);
    }
}
