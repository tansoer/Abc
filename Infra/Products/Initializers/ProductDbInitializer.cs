using Abc.Infra.Products.Initializers.Leeds;
using Abc.Infra.Products.Initializers.Loinc;

namespace Abc.Infra.Products.Initializers {

    public class ProductDbInitializer :DbInitializer {

        public static void Initialize(ProductDb db) {
            LeedsProductsCatalogInitializer.Initialize(db);
            LoincCatalogInitializer.Initialize(db);
        }
    }
}