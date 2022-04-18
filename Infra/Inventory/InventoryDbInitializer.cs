
namespace Abc.Infra.Inventory {
    public sealed class InventoryDbInitializer: DbInitializer {

        public static void Initialize(InventoryDb db) {
            if (db is null) return;
        }
    }
}
