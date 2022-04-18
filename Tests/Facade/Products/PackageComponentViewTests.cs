using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class PackageComponentViewTests :SealedTests<PackageComponentView, EntityBaseView> {
        [TestMethod] public void ProductTypeIdTest() => isNullableProperty<string>("Product Type");
        [TestMethod] public void PackageTypeIdTest() => isNullableProperty<string>("Package Type");
    }
}
