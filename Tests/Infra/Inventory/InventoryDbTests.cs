using Abc.Data.Inventory;
using Abc.Infra;
using Abc.Infra.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Inventory {

    [TestClass]
    public class InventoryDbTests :DbTests<InventoryDb, BaseDb<InventoryDb>> {
        private class testClass :InventoryDb {
            public testClass(DbContextOptions<InventoryDb> o) : base(o) { }
            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);
                return mb;
            }
        }
        protected override InventoryDb createObject() {
            options = new DbContextOptionsBuilder<InventoryDb>().UseInMemoryDatabase("TestDb").Options;
            return new InventoryDb(options);
        }
        [TestMethod]
        public void InitializeTablesTest() {
            InventoryDb.InitializeTables(null);
            var o = new testClass(options);
            var builder = o.RunOnModelCreating();
            InventoryDb.InitializeTables(builder);
            testEntity<InventoryEntryData>(builder);
            testEntity<ReservationData>(builder);
            testEntity<ReservationRequestData>(builder);
            testEntity<RestockPolicyData>(builder);
            testEntity<OutstandingOrderData>(builder);
            testEntity<ReservationReceiverData>(builder);
            testEntity<CapacityManagerData>(builder);
        }
        [TestMethod] public void InventoriesTest() => isNullable<DbSet<InventoryData>>();
        [TestMethod] public void InventoryEntriesTest() => isNullable<DbSet<InventoryEntryData>>();
        [TestMethod] public void ReservationsTest() => isNullable<DbSet<ReservationData>>();
        [TestMethod] public void ReservationRequestsTest() => isNullable<DbSet<ReservationRequestData>>();
        [TestMethod] public void RestockPoliciesTest() => isNullable<DbSet<RestockPolicyData>>();
        [TestMethod] public void OutstandingOrdersTest() => isNullable<DbSet<OutstandingOrderData>>();
        [TestMethod] public void ReservationReceiversTest() => isNullable<DbSet<ReservationReceiverData>>();
        [TestMethod] public void CapacityManagersTest() => isNullable<DbSet<CapacityManagerData>>();
    }
}