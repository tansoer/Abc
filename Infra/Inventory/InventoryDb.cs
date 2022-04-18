using Abc.Data.Inventory;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Inventory {
    public class InventoryDb :BaseDb<InventoryDb> {
        public DbSet<InventoryData> Inventories { get; set; }
        public DbSet<InventoryEntryData> InventoryEntries { get; set; }
        public DbSet<ReservationData> Reservations { get; set; }
        public DbSet<ReservationRequestData> ReservationRequests { get; set; }
        public DbSet<RestockPolicyData> RestockPolicies { get; set; }
        public DbSet<OutstandingOrderData> OutstandingOrders { get; set; }
        public DbSet<ReservationReceiverData> ReservationReceivers { get; set; }
        public DbSet<CapacityManagerData> CapacityManagers { get; set; }

        public InventoryDb(DbContextOptions<InventoryDb> o): base(o) { }

        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            InitializeTables(b);
        }
        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            var s = "Inventory";
            toTable<InventoryData>(b, nameof(Inventories), s);
            toTable<InventoryEntryData>(b, nameof(InventoryEntries), s);
            toTable<ReservationData>(b, nameof(Reservations), s);
            toTable<ReservationRequestData>(b, nameof(ReservationRequests), s);
            toTable<RestockPolicyData>(b, nameof(RestockPolicies), s);
            toTable<OutstandingOrderData>(b, nameof(OutstandingOrders), s);
            toTable<ReservationReceiverData>(b, nameof(ReservationReceivers), s);
            toTable<CapacityManagerData>(b, nameof(CapacityManagers), s);
        }
    }
}

