using System.ComponentModel;

namespace Abc.Data.Products {

    public enum ProductKind {

        Unspecified = 0,
        [Description("Product")] Product = 1,
        [Description("Measured product")] MeasuredProduct = 2,
        [Description("Unique product")] UniqueProduct = 3,
        [Description("Identical product")] IdenticalProduct = 4,
        [Description("Service")] Service = 5,
        [Description("Container")] Container = 6,
        [Description("Package")] Package = 9
    }
}