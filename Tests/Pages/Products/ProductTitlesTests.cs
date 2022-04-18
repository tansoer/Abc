using Abc.Pages.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Products {
    [TestClass]
    public class ProductTitlesTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ProductTitles);
        [TestMethod] public void ProductCatalogsTest() => Assert.AreEqual("Product catalogs", ProductTitles.ProductCatalogs);
        [TestMethod] public void CatalogEntriesTest() => Assert.AreEqual("Catalog entries", ProductTitles.CatalogEntries);
        [TestMethod] public void CatalogedProductsTest() => Assert.AreEqual("Cataloged products", ProductTitles.CatalogedProducts);
        [TestMethod] public void FeaturesTest() => Assert.AreEqual("Features", ProductTitles.Features);
        [TestMethod] public void FeatureTypesTest() => Assert.AreEqual("Feature types", ProductTitles.FeatureTypes);
        [TestMethod] public void PossibleFeatureValuesTest() => Assert.AreEqual("Possible feature values", ProductTitles.PossibleFeatureValues);
        [TestMethod] public void ProductsTest() => Assert.AreEqual("Products", ProductTitles.Products);
        [TestMethod] public void ProductTypesTest() => Assert.AreEqual("Product types", ProductTitles.ProductTypes);
        [TestMethod] public void ProductCategoriesTest() => Assert.AreEqual("Product categories", ProductTitles.ProductCategories);
        [TestMethod] public void BatchesTest() => Assert.AreEqual("Batches", ProductTitles.Batches);
        [TestMethod] public void BatchCheckedByTest() => areEqual("Batch checked by", ProductTitles.BatchCheckedBy);
        [TestMethod] public void ProductRelationshipTypesTest() => areEqual("Product relationship types", ProductTitles.ProductRelationshipTypes);
        [TestMethod] public void ProductRelationshipsTest() => areEqual("Product relationships", ProductTitles.ProductRelationships);
        [TestMethod] public void PackageComponentsTest() => areEqual("Package components", ProductTitles.PackageComponents);
        [TestMethod] public void ProductInclusionRulesTest() => areEqual("Product inclusion rules", ProductTitles.ProductInclusionRules);
        [TestMethod] public void ProductSetsTest() => areEqual("Product sets", ProductTitles.ProductSets);
        [TestMethod] public void ProductSetContentsTest() => areEqual("Product set contents", ProductTitles.ProductSetContents);
        [TestMethod] public void PackageContentsTest() => areEqual("Package contents", ProductTitles.PackageContents);
        [TestMethod] public void ContainerItemsTest() => areEqual("Container items", ProductTitles.ContainerItems);
        [TestMethod] public void PricesTest() => areEqual("Prices", ProductTitles.Prices);
        [TestMethod] public void PriceEndorsementsTest() => areEqual("Price endorsements", ProductTitles.PriceEndorsements);
    }
}
