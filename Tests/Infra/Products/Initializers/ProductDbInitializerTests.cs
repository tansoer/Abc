using Abc.Infra.Products;
using Abc.Infra.Products.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace Abc.Tests.Infra.Products.Initializers {

    [TestClass] public class ProductDbInitializerTests :DbInitializerTests<ProductDb> {
        public ProductDbInitializerTests() {
            type = typeof(ProductDbInitializer);
            db = new ProductDb(options);
            removeAll(db.Products);
            removeAll(db.ProductTypes);
            removeAll(db.Features);
            removeAll(db.FeatureTypes);
            removeAll(db.PossibleFeatureValues);
            removeAll(db.Batches);
            removeAll(db.BatchCheckedByParties);
            removeAll(db.CatalogedProducts);
            removeAll(db.CatalogEntries);
            removeAll(db.ProductCategories);
            removeAll(db.ProductCatalogs);
            removeAll(db.PackageComponents);
            removeAll(db.PackageContents);
            removeAll(db.ProductSets);
            removeAll(db.ProductSetContents);
            removeAll(db.ProductInclusions);
            removeAll(db.ProductRelationshipTypes);
            removeAll(db.ProductRelationships);
            removeAll(db.Prices);
            removeAll(db.PriceEndorsements);
            removeAll(db.ContainerItems);
            ProductDbInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void ProductsTest() => areEqual(0, db.Products.Count());
        [TestMethod] public void ProductTypesTest() => areEqual(4245, db.ProductTypes.Count());
        [TestMethod] public void FeaturesTest() => areEqual(0, db.Features.Count());
        [TestMethod] public void FeatureSpecsTest() => areEqual(0, db.FeatureSpecs.Count());
        [TestMethod] public void FeatureTypesTest() => areEqual(0, db.FeatureTypes.Count());
        [TestMethod] public void PossibleFeatureValuesTest() => areEqual(0, db.PossibleFeatureValues.Count());
        [TestMethod] public void BatchesTest() => areEqual(0, db.Batches.Count());
        [TestMethod] public void BatchCheckedByPartiesTest() => areEqual(0, db.BatchCheckedByParties.Count());
        [TestMethod] public void CatalogedProductsTest() => areEqual(70, db.CatalogedProducts.Count());
        [TestMethod] public void CatalogEntriesTest() => areEqual(14, db.CatalogEntries.Count());
        [TestMethod] public void ProductCategoriesTest() => areEqual(10, db.ProductCategories.Count());
        [TestMethod] public void ProductCatalogsTest() => areEqual(4, db.ProductCatalogs.Count());
        [TestMethod] public void PackageComponentsTest() => areEqual(435, db.PackageComponents.Count());
        [TestMethod] public void PackageContentsTest() => areEqual(0, db.PackageContents.Count());
        [TestMethod] public void ProductSetsTest() => areEqual(435, db.ProductSets.Count());
        [TestMethod] public void ProductSetContentsTest() => areEqual(435, db.ProductSetContents.Count());
        [TestMethod] public void ProductInclusionsTest() => areEqual(3631, db.ProductInclusions.Count());
        [TestMethod] public void ProductRelationshipTypesTest() => areEqual(0, db.ProductRelationshipTypes.Count());
        [TestMethod] public void ProductRelationshipsTest() => areEqual(0, db.ProductRelationships.Count());
        [TestMethod] public void PricesTest() => areEqual(0, db.Prices.Count());
        [TestMethod] public void PriceEndorsementsTest() => areEqual(0, db.PriceEndorsements.Count());
        [TestMethod] public void ContainerItemsTest() => areEqual(0, db.ContainerItems.Count());
    }
}
