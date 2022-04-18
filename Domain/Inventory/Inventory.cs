using Abc.Data.Inventory;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Exceptions;
using Abc.Domain.Products;
using System.Collections.Generic;
using System.Linq;

namespace Abc.Domain.Inventory {
    public sealed class Inventory: Entity<InventoryData> {
        internal static string inventoryId => nameOf<InventoryItemData>(x => x.InventoryId);
        public Inventory(): this(null) {}
        public Inventory(InventoryData d): base(d) {}
        public IReadOnlyList<IProductType> ProductTypes => list(Entries, x => x.ProductType);
        public IReadOnlyList<InventoryEntry> Entries 
            => list<IInventoryEntriesRepo, InventoryEntry>(inventoryId, Id);
        public IReadOnlyList<Reservation> Reservations
            => list<IReservationsRepo, Reservation>(inventoryId, Id);
        public IReadOnlyList<ReservationRequest> Requests
            => list<IReservationRequestsRepo, ReservationRequest>(inventoryId, Id);
        public void AddEntry(InventoryEntryData d) {
            d = validateEntry(d);
            add<IInventoryEntriesRepo, InventoryEntry>(InventoryEntryFactory.Create(d));
        }
        private InventoryEntryData validateEntry(InventoryEntryData d) {
            isNotNull(ref d);
            isCorrectProductType(ref d);
            isCorrectInventory(ref d);
            return d;
        }
        private static void isNotNull(ref InventoryEntryData d) {
            if (d is null) throw new NoInventoryEntryData();
        }
        private void isCorrectProductType(ref InventoryEntryData d) {
            var t = isProductTypeSpecified(d);
            isProductTypeCorrect(d, t);
            isProductTypeUnique(t);
        }
        private static IProductType isProductTypeSpecified(InventoryEntryData d) {
            var p = item<IProductTypesRepo, IProductType>(d.ProductTypeId);
            if (p.IsUnspecified) throw new UnknownEntryProduct();
            return p;
        }
        private static void isProductTypeCorrect(InventoryEntryData d, IProductType t) {
            if (d.InventoryEntryType == ProductKind.Unspecified)
                d.InventoryEntryType = t.ProductKind;
            if (d.InventoryEntryType != t.ProductKind)
                throw new UnspecifiedEntryProduct();
        }
        private void isProductTypeUnique(IProductType t) {
            var x = ProductTypes.FirstOrDefault(x => x.Id == t.Id);
            if (x != null) throw new DuplicatedEntryProduct();
        }
        private void isCorrectInventory(ref InventoryEntryData d) {
            if (Unspecified.IsUnspecified(d.InventoryId)) d.InventoryId = Id;
            if (d.InventoryId != Id) throw new UnspecifiedEntryInventory();
        }
    }
}
