using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.EntityFrameworkCore;

namespace Abc.Infra.Products {
    public class ProductDb : BaseDb<ProductDb> {

        public DbSet<ProductData> Products { get; set; }
        public DbSet<ProductTypeData> ProductTypes { get; set; }
        public DbSet<FeatureData> Features { get; set; }
        public DbSet<FeatureTypeData> FeatureTypes { get; set; }
        public DbSet<PossibleFeatureValueData> PossibleFeatureValues { get; set; }
        public DbSet<FeatureSpecData> FeatureSpecs { get; set; }
        public DbSet<BatchData> Batches { get; set; }
        public DbSet<BatchCheckedByData> BatchCheckedByParties { get; set; }
        public DbSet<CatalogedProductData> CatalogedProducts { get; set; }
        public DbSet<CatalogEntryData> CatalogEntries { get; set; }
        public DbSet<ProductCategoryData> ProductCategories { get; set; }
        public DbSet<ProductCatalogData> ProductCatalogs { get; set; }
        public DbSet<PackageComponentData> PackageComponents { get; set; }
        public DbSet<PackageContentData> PackageContents { get; set; }
        public DbSet<ProductSetData> ProductSets { get; set; }
        public DbSet<ProductSetContentData> ProductSetContents { get; set; }
        public DbSet<ProductInclusionRuleData> ProductInclusions { get; set; }
        public DbSet<ProductRelationshipTypeData> ProductRelationshipTypes { get; set; }
        public DbSet<ProductRelationshipData> ProductRelationships { get; set; }
        public DbSet<PriceData> Prices { get; set; }
        public DbSet<PriceEndorsementData> PriceEndorsements { get; set; }
        public DbSet<ContainerItemData> ContainerItems { get; set; }
        public ProductDb(DbContextOptions<ProductDb> o) : base(o) { }
        protected override void OnModelCreating(ModelBuilder b) {
            base.OnModelCreating(b);
            InitializeTables(b);
        }
        public static void InitializeTables(ModelBuilder b) {
            if (b is null) return;
            var s = "Product";
            toTable<ProductData>(b,nameof(Products),s);
            toTable<ProductInclusionRuleData>(b,nameof(ProductInclusions),s);
            toTable<ProductSetData>(b,nameof(ProductSets),s);
            toTable<ProductTypeData>(b,nameof(ProductTypes),s);
            toTable<FeatureTypeData>(b,nameof(FeatureTypes),s);
            toTable<PossibleFeatureValueData>(b,nameof(PossibleFeatureValues),s);
            toTable<FeatureData>(b,nameof(Features), s);
            toTableWithOwnsOne<FeatureSpecData, ValueData>(b, nameof(FeatureSpecs), s, x => x.Value);
            toTable<BatchData>(b,nameof(Batches),s);
            toTable<BatchCheckedByData>(b,nameof(BatchCheckedByParties),s);
            toTable<CatalogedProductData>(b,nameof(CatalogedProducts),s);
            toTable<CatalogEntryData>(b,nameof(CatalogEntries),s);
            toTable<ProductCategoryData>(b,nameof(ProductCategories),s);
            toTable<ProductCatalogData>(b,nameof(ProductCatalogs),s);
            toTable<PackageContentData>(b,nameof(PackageContents),s);
            toTable<PackageComponentData>(b,nameof(PackageComponents),s);
            toTable<ProductSetContentData>(b,nameof(ProductSetContents),s);
            toTable<ProductRelationshipData>(b,nameof(ProductRelationships),s);
            toTable<ProductRelationshipTypeData>(b,nameof(ProductRelationshipTypes),s);
            toTableWithDecimalCol<PriceData>(b,nameof(Prices), s, x => x.Amount);
            toTable<PriceEndorsementData>(b,nameof(PriceEndorsements),s);
            toTable<PriceEndorsementData>(b,nameof(PriceEndorsements),s);
            toTable<ContainerItemData>(b,nameof(ContainerItems),s);
        }
    }
}
