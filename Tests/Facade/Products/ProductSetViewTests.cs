using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class ProductSetViewTests :SealedTests<ProductSetView, EntityBaseView>{
        [TestMethod] public void PackageTypeIdTest() => isNullableProperty<string>("Package Type");
        [TestMethod] public void ToStringTest()
            => areEqual(new ProductSetViewFactory().Create(obj).ToString(), obj.ToString());
    }
}
