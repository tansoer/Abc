using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {

    [TestClass]
    public class ProductCategoryViewTests : SealedTests<ProductCategoryView, EntityBaseView> {

        [TestMethod] public void BaseCategoryIdTest() => isNullableProperty<string>("Base Category");

    }
}