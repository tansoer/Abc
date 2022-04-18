
using System;

namespace Abc.Infra.Orders {
    public sealed class OrderDbInitializer: DbInitializer {

        public static void Initialize(OrderDb db) {
            if (db is null) return;
            //initializeTermsAndConditionsTypes(db);
        }

    }
}
