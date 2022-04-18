using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Facade.Common;

namespace Abc.Facade.Products {

    public sealed class ProductTypeViewFactory :AbstractViewFactory<ProductTypeData, IProductType, ProductTypeView> {
        protected internal override IProductType toObject(ProductTypeData d) => ProductTypeFactory.Create(d);
    }
}