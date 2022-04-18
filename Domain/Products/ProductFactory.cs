using Abc.Data.Products;
using Abc.Domain.Products.Packets;

namespace Abc.Domain.Products {
    public static class ProductFactory {
        public static IProduct Create(ProductData d) => d?.ProductKind switch {
            ProductKind.Product => new Product(d),
            ProductKind.MeasuredProduct => new MeasuredProduct(d),
            ProductKind.UniqueProduct => new UniqueProduct(d),
            ProductKind.IdenticalProduct => new IdenticalProduct(d),
            ProductKind.Service => new Service(d),
            ProductKind.Package => new Package(d),
            ProductKind.Container => new Container(d),
            _ => error(d)
        };
        private static IProduct error(ProductData d) => new ProductError(d);
    }
}