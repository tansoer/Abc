using Abc.Domain.Inventory;
using Microsoft.Extensions.DependencyInjection;

namespace Abc.Infra.Inventory {
    public static class InventoryDbRepos {
        public static void Register(IServiceCollection c) {
            c.AddTransient<IInventoriesRepo, InventoriesRepo>();
            c.AddTransient<IInventoryEntriesRepo, InventoryEntriesRepo>();
            c.AddTransient<IReservationsRepo, ReservationsRepo>();
            c.AddTransient<IReservationRequestsRepo, ReservationRequestsRepo>();
            c.AddTransient<IRestockPoliciesRepo, RestockPoliciesRepo>();
            c.AddTransient<IOutstandingOrdersRepo, OutstandingOrdersRepo>();
            c.AddTransient<IReservationReceiversRepo, ReservationReceiversRepo>();
            c.AddTransient<ICapacityManagersRepo, CapacityManagersRepo>();
        }
    }
}
