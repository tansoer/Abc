using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products;
using Abc.Domain.Products.Catalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Catalog {

    [TestClass]
    public class CatalogEntryTests : SealedTests<CatalogEntry, Entity<CatalogEntryData>> {

        protected override CatalogEntry createObject() => new (GetRandom.ObjectOf<CatalogEntryData>());

        [TestMethod] public void CatalogIdTest() => isReadOnly(obj.Data.CatalogId);

        [TestMethod] public void CategoryIdTest() => isReadOnly(obj.Data.CategoryId);

        [TestMethod]
        public async Task CategoryTest() =>
            await testItemAsync<ProductCategoryData, ProductCategory, IProductCategoriesRepo>(
                obj.CategoryId, () => obj.Category.Data, d => new ProductCategory(d));

        [TestMethod]
        public async Task CatalogedProductTypesTest()
            => await testListAsync<CatalogedProduct, CatalogedProductData, ICatalogedProductsRepo>(
                obj, nameof(obj.CatalogedProductTypes),
                x => x.CatalogEntryId = obj.Id,
                d => new CatalogedProduct(d));


        [TestMethod]
        public async Task ProductTypesTest() {
            isReadOnly(obj, nameof(obj.ProductTypes));
            Assert.AreEqual(0, obj.ProductTypes.Count);
            await CatalogedProductTypesTest();

            foreach (var e in obj.CatalogedProductTypes) {
                var d = GetRandom.ObjectOf<ProductTypeData>();
                d.Id = e.ProductTypeId;
                await addAsync<IProductTypesRepo, IProductType>(new ProductType(d));
            }

            Assert.AreNotEqual(0, obj.ProductTypes.Count);
            Assert.AreEqual(obj.CatalogedProductTypes.Count, obj.ProductTypes.Count);
        }

    }

}