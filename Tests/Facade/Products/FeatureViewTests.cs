using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {

    [TestClass]
    public class FeatureViewTests : SealedTests<FeatureView, FeatureValueView> {
        [TestMethod] public void ProductIdTest() => isNullableProperty<string>("Product");
    }
}