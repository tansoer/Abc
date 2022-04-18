using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Catalog {

    public sealed class CatalogedProduct : Entity<CatalogedProductData> {

        public CatalogedProduct() : this(null) { }
        public CatalogedProduct(CatalogedProductData d) : base(d) { }

        public string CatalogEntryId => data?.CatalogEntryId ?? Unspecified.String;

        public string ProductTypeId => data?.ProductTypeId ?? Unspecified.String;

        public CatalogEntry CatalogEntry =>
            new GetFrom<ICatalogEntriesRepo, CatalogEntry>().ById(CatalogEntryId);

        public IProductType ProductType =>
            new GetFrom<IProductTypesRepo, IProductType>().ById(ProductTypeId);

    }

}