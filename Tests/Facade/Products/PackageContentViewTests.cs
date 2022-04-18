using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass] public class PackageContentViewTests :SealedTests<PackageContentView, EntityBaseView>{
        [TestMethod] public void ProductIdTest() => isNullableProperty<string>("Product");
        [TestMethod] public void PackageIdTest() => isNullableProperty<string>("Package");
        [TestMethod] public void ComponentIdTest() => isNullableProperty<string>("Component");
    }
}
