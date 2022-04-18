using System.Collections.Generic;
using System.Linq;
using Abc.Aids.Reflection;
using Abc.Data.Products;
using Abc.Domain.Common;

namespace Abc.Domain.Products.Catalog {
    public sealed class CatalogEntry : Entity<CatalogEntryData> {
        internal static string entryId
            => GetMember.Name<CatalogedProductData>(x => x.CatalogEntryId);
        public CatalogEntry() : base(null) { }
        public CatalogEntry(CatalogEntryData d) : base(d) { }
        public string CatalogId => data?.CatalogId ?? Unspecified.String;
        public string CategoryId => data?.CategoryId ?? Unspecified.String;
        public ProductCategory Category =>
            new GetFrom<IProductCategoriesRepo, ProductCategory>().ById(CategoryId);
        public IReadOnlyList<CatalogedProduct> CatalogedProductTypes =>
            new GetFrom<ICatalogedProductsRepo, CatalogedProduct>().ListBy(entryId, Id);
        public IReadOnlyList<IProductType> ProductTypes
            => CatalogedProductTypes.Select(e => e.ProductType).ToList();
    }
}