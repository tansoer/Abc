
namespace Abc.Infra.Roles {
    public sealed class RoleDbInitializer: DbInitializer {

        public static void Initialize(RoleDb db) {
            if (db is null) return;
            //TODO initializeCountries(db);
        }
    }
}
