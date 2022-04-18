using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass] public class FeatureDataTests : SealedTests<FeatureData, FeatureValueData> {
        [TestMethod] public void ProductIdTest() => isNullable<string>();
    }
}