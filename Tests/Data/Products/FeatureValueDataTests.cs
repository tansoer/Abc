using Abc.Data.Common;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass] public class FeatureValueDataTests :AbstractTests<FeatureValueData, EntityBaseData> {
        private class testClass :FeatureValueData { }
        protected override FeatureValueData createObject() => random<testClass>();
        [TestMethod] public void FeatureTypeIdTest() => isNullable<string>();
        [TestMethod] public void ValueSpecIdTest() => isNullable<string>();
    }
}
