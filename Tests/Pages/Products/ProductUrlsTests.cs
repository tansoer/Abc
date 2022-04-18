using Abc.Pages.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Products {

    [TestClass]
    public class ProductUrlsTests :TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(ProductUrls);
        [TestMethod] public void ProductCatalogsTest() => Assert.AreEqual("/Products/ProductCatalogs", ProductUrls.ProductCatalogs);
        [TestMethod] public void CatalogEntriesTest() => Assert.AreEqual("/Products/CatalogEntries", ProductUrls.CatalogEntries);
        [TestMethod] public void CatalogedProductsTest() => Assert.AreEqual("/Products/CatalogedProducts", ProductUrls.CatalogedProducts);
        [TestMethod] public void FeaturesTest() => Assert.AreEqual("/Products/Features", ProductUrls.Features);
        [TestMethod] public void FeatureTypesTest() => Assert.AreEqual("/Products/FeatureTypes", ProductUrls.FeatureTypes);
        [TestMethod] public void PossibleFeatureValuesTest() => Assert.AreEqual("/Products/PossibleFeatureValues", ProductUrls.PossibleFeatureValues);
        [TestMethod] public void ProductsTest() => Assert.AreEqual("/Products/Products", ProductUrls.Products);
        [TestMethod] public void ProductTypesTest() => Assert.AreEqual("/Products/ProductTypes", ProductUrls.ProductTypes);
        [TestMethod] public void ProductCategoriesTest() => Assert.AreEqual("/Products/ProductCategories", ProductUrls.ProductCategories);
        [TestMethod] public void BatchesTest() => Assert.AreEqual("/Products/Batches", ProductUrls.Batches);
        [TestMethod] public void BatchCheckedByTest() => areEqual("/Products/BatchCheckedBy", ProductUrls.BatchCheckedBy);
        [TestMethod] public void ProductRelationshipTypesTest() => areEqual("/Products/ProductRelationshipTypes", ProductUrls.ProductRelationshipTypes);
        [TestMethod] public void ProductRelationshipsTest() => areEqual("/Products/ProductRelationships", ProductUrls.ProductRelationships);
        [TestMethod] public void PackageComponentsTest() => areEqual("/Products/PackageComponents", ProductUrls.PackageComponents);
        [TestMethod] public void ProductInclusionRulesTest() => areEqual("/Products/ProductInclusionRules", ProductUrls.ProductInclusionRules);
        [TestMethod] public void ProductSetsTest() => areEqual("/Products/ProductSets", ProductUrls.ProductSets);
        [TestMethod] public void ProductSetContentsTest() => areEqual("/Products/ProductSetContents", ProductUrls.ProductSetContents);
        [TestMethod] public void PackageContentsTest() => areEqual("/Products/PackageContents", ProductUrls.PackageContents);
        [TestMethod] public void ContainerItemsTest() => areEqual("/Products/ContainerItems", ProductUrls.ContainerItems);
        [TestMethod] public void PricesTest() => areEqual("/Products/Prices", ProductUrls.Prices);
        [TestMethod] public void PriceEndorsementsTest() => areEqual("/Products/PriceEndorsements", ProductUrls.PriceEndorsements);
    }

}