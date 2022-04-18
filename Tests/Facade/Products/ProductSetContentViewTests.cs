using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class ProductSetContentViewTests 
        :SealedTests<ProductSetContentView, EntityBaseView> {
        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product Type");
        [TestMethod] public void ProductSetIdTest() => isNullableProperty<string>("Product Set");
    }
}
