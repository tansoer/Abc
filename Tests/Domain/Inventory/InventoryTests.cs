using Abc.Aids.Random;
using Abc.Data.Inventory;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Exceptions;
using Abc.Domain.Inventory;
using Abc.Domain.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Inventory {
    [TestClass]
    public class InventoryTests
        : SealedTests<Abc.Domain.Inventory.Inventory, Entity<InventoryData>> {

        protected override Abc.Domain.Inventory.Inventory createObject()
            => new (random<InventoryData>());

        [TestMethod]
        public void ProductTypesTest() {
            isReadOnly();
            areEqual(0, obj.Entries.Count);
            areEqual(0, obj.ProductTypes.Count);
            for (int i = 0; i < 10; i++) {
                addProductTypeWithRelatedEntry();
            }
            areNotEqual(0, obj.Entries.Count);
            areNotEqual(0, obj.ProductTypes.Count);
            areEqual(obj.Entries.Count, obj.ProductTypes.Count);
        }

        private void addProductTypeWithRelatedEntry() {
            var productTypeData = random<ProductTypeData>();
            var entryData = random<InventoryEntryData>();
            entryData.InventoryId = obj.Id;
            entryData.ProductTypeId = productTypeData.Id;
            GetRepo.Instance<IProductTypesRepo>().Add(ProductTypeFactory.Create(productTypeData));
            GetRepo.Instance<IInventoryEntriesRepo>().Add(InventoryEntryFactory.Create(entryData));
        }

        [TestMethod] public async Task EntriesTest()
            => await testListAsync<InventoryEntry, InventoryEntryData, IInventoryEntriesRepo>(
                x => x.InventoryId = obj.Id, InventoryEntryFactory.Create);

        [TestMethod] public async Task ReservationsTest()
            => await testListAsync<Reservation, ReservationData, IReservationsRepo>(
                x => x.InventoryId = obj.Id, x => new Reservation(x));

        [TestMethod] public async Task RequestsTest()
            => await testListAsync<ReservationRequest, ReservationRequestData, IReservationRequestsRepo>(
                x => x.InventoryId = obj.Id, x => new ReservationRequest(x));

        [TestMethod] public void AddEntryNoDataTest() 
            => throwsException<NoInventoryEntryData>(() => addEntry(null));

        [TestMethod] public void AddEntryUnknownProductTest() 
            => throwsException<UnknownEntryProduct>( () => addEntry(rndEntry));

        [TestMethod] public async Task AddEntryUnspecifiedProductTest() {
            var i = rndEntry;
            var p = await rndProduct(i);
            i.ProductTypeId = p.Id;
            throwsException<UnspecifiedEntryProduct>(() => addEntry(i));
        }
        [TestMethod]
        public async Task AddEntryDuplicatedProductTest() {
            var i1 = rndEntry;
            var i2 = rndEntry;
            var p = await rndProduct(i1);
            set(i1, p.ProductKind, p.Id);
            set(i2, p.ProductKind, p.Id, obj.Id);
            await addAsync<IInventoryEntriesRepo, InventoryEntry>(InventoryEntryFactory.Create(i2));
            throwsException<DuplicatedEntryProduct>(() => addEntry(i1));
        }

        private static void set(InventoryEntryData i, ProductKind k, string pId, string iId = null) {
            i.InventoryEntryType = k;
            i.ProductTypeId = pId;
            i.InventoryId = iId;
        }

        [TestMethod]
        public async Task AddEntryUnspecifiedInventoryTest() {
            var i1 = rndEntry;
            var i2 = rndEntry;
            var p1 = await rndProduct(i1);
            var p2 = await rndProduct(i2);
            set(i1, p1.ProductKind, p1.Id, rndStr);
            set(i2, p2.ProductKind, p2.Id, obj.Id);
            await addAsync<IInventoryEntriesRepo, InventoryEntry>(InventoryEntryFactory.Create(i2));
            throwsException<UnspecifiedEntryInventory>(() => addEntry(i1));
        }
        [TestMethod]
        public async Task AddEntryTest() {
            var i1 = rndEntry;
            var i2 = rndEntry;
            var p1 = await rndProduct(i1);
            var p2 = await rndProduct(i2);
            set(i1, p1.ProductKind, p1.Id, obj.Id);
            set(i2, p2.ProductKind, p2.Id, obj.Id);
            await addAsync<IInventoryEntriesRepo, InventoryEntry>(InventoryEntryFactory.Create(i2));
            areEqual(1, obj.Entries.Count);
            addEntry(i1);
            areEqual(2, obj.Entries.Count);
        }
        [TestMethod]
        public async Task AddEntryUnspecifiedTest() {
            var i1 = rndEntry;
            var i2 = rndEntry;
            var p1 = await rndProduct(i1);
            var p2 = await rndProduct(i2);
            set(i1, ProductKind.Unspecified, p1.Id);
            set(i2, p2.ProductKind, p2.Id, obj.Id);
            await addAsync<IInventoryEntriesRepo, InventoryEntry>(InventoryEntryFactory.Create(i2));
            areEqual(1, obj.Entries.Count);
            addEntry(i1);
            areEqual(2, obj.Entries.Count);
        }

        private void addEntry(InventoryEntryData d) => obj.AddEntry(d);
        private static InventoryEntryData rndEntry => random<InventoryEntryData>();
        private static async Task<ProductTypeData> rndProduct(InventoryEntryData i) {
            var p = random<ProductTypeData>();
            while (i.InventoryEntryType == ProductKind.Unspecified
                || i.InventoryEntryType == p.ProductKind) {
                i.InventoryEntryType = GetRandom.EnumOf<ProductKind>();
            }
            await addAsync<IProductTypesRepo, IProductType>(ProductTypeFactory.Create(p));
            return p;
        }
    }
}
