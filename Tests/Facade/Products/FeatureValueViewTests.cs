using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {

    [TestClass] public class FeatureValueViewTests: 
        AbstractTests<FeatureValueView, EntityBaseView> {
        private class testClass : FeatureValueView { }
        protected override FeatureValueView createObject() => GetRandom.ObjectOf<testClass>();
        [TestMethod] public void FeatureTypeIdTest() => isNullableProperty<string>("Feature type");
        [TestMethod] public void ValueSpecificationIdTest() 
            => isNullableProperty<string>("Value");
    }
}
