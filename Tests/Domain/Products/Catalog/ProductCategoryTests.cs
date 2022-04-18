using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Catalog;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Catalog {

    [TestClass]
    public class ProductCategoryTests : SealedTests<ProductCategory, Entity<ProductCategoryData>> {

        protected override ProductCategory createObject() =>
            new (GetRandom.ObjectOf<ProductCategoryData>());

        [TestMethod] public void BaseCategoryIdTest() => isReadOnly(obj.Data.BaseCategoryId);

        [TestMethod]
        public async Task BaseCategoryTest() =>
            await testItemAsync<ProductCategoryData, ProductCategory, IProductCategoriesRepo>(
                obj.BaseCategoryId, () => obj.BaseCategory.Data,
                d => new ProductCategory(d));

    }

}