using Abc.Data.Products;
using Abc.Domain.Products.Packets;

namespace Abc.Domain.Products {

    public static class ProductTypeFactory {

        public static IProductType Create(ProductTypeData d) {
            if (d is null) return error(null);

            return d.ProductKind switch
            {
                ProductKind.Unspecified => error(d),
                ProductKind.Product => new ProductType(d),
                ProductKind.MeasuredProduct => new MeasuredProductType(d),
                ProductKind.UniqueProduct => new UniqueProductType(d),
                ProductKind.IdenticalProduct => new IdenticalProductType(d),
                ProductKind.Service => new ServiceType(d),
                ProductKind.Package => new PackageType(d),
                ProductKind.Container => new ContainerType(d),
                _ => error(d)
            };
        }
        private static IProductType error(ProductTypeData d) => new ProductTypeError(d);

    }

}
