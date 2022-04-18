using Abc.Data.Products;
using Abc.Infra;
using Abc.Infra.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Products {
    //TODO : Kõik Db-d tuleb nii testida
    [TestClass] public class ProductDbTests :DbTests<ProductDb, BaseDb<ProductDb>> {
        private ModelBuilder builder;
        private class testClass : ProductDb {
            public testClass(DbContextOptions<ProductDb> o) : base(o) { }
            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);
                return mb;
            }
        }
        protected override ProductDb createObject() {
            options = new DbContextOptionsBuilder<ProductDb>().UseInMemoryDatabase("TestDb").Options;
            InitializeTablesTest();
            return new ProductDb(options);
        }
        public void InitializeTablesTest() {
            ProductDb.InitializeTables(null);
            var o = new testClass(options);
            builder = o.RunOnModelCreating();
            ProductDb.InitializeTables(builder);
        }
        [TestMethod] public void ProductsTest() => hasDbSet<ProductData>(builder);
        [TestMethod] public void ProductTypesTest() => hasDbSet<ProductTypeData>(builder);
        [TestMethod] public void FeaturesTest() => hasDbSet<FeatureData>(builder);
        [TestMethod] public void FeatureTypesTest() => hasDbSet<FeatureTypeData>(builder);
        [TestMethod] public void PossibleFeatureValuesTest() => hasDbSet<PossibleFeatureValueData>(builder);
        [TestMethod] public void BatchesTest() => hasDbSet<BatchData>(builder);
        [TestMethod] public void BatchCheckedByPartiesTest() =>hasDbSet<BatchCheckedByData>(builder);
        [TestMethod] public void CatalogedProductsTest() => hasDbSet<CatalogedProductData>(builder);
        [TestMethod] public void CatalogEntriesTest() => hasDbSet<CatalogEntryData>(builder);
        [TestMethod] public void ProductCategoriesTest() => hasDbSet<ProductCategoryData>(builder);
        [TestMethod] public void ProductCatalogsTest() => hasDbSet<ProductCatalogData>(builder);
        [TestMethod] public void PackageContentsTest() => hasDbSet<PackageContentData>(builder);
        [TestMethod] public void PackageComponentsTest() => hasDbSet<PackageComponentData>(builder);
        [TestMethod] public void ProductSetsTest() => hasDbSet<ProductSetData>(builder);
        [TestMethod] public void ProductSetContentsTest() => hasDbSet<ProductSetContentData>(builder);
        [TestMethod] public void ProductInclusionsTest() => hasDbSet<ProductInclusionRuleData>(builder);
        [TestMethod] public void ProductRelationshipsTest() => hasDbSet<ProductRelationshipData>(builder);
        [TestMethod] public void ProductRelationshipTypesTest() => hasDbSet<ProductRelationshipTypeData>(builder);
        [TestMethod] public void PricesTest() => hasDbSet<PriceData>(builder);
        [TestMethod] public void PriceEndorsementsTest() => hasDbSet<PriceEndorsementData>(builder);
        [TestMethod] public void FeatureSpecsTest() => hasDbSet<FeatureSpecData>(builder);
        [TestMethod] public void ContainerItemsTest() => hasDbSet<ContainerItemData>(builder);
    }
}
