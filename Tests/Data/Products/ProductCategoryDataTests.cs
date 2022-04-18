using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class ProductCategoryDataTests : SealedTests<ProductCategoryData, EntityBaseData> {
        [TestMethod] public void BaseCategoryIdTest() => isNullable<string>();

    }

}