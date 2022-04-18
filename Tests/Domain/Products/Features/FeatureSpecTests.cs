using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Products.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products.Features {
    [TestClass] public class FeatureSpecTests :
        SealedTests<FeatureSpec, Entity<FeatureSpecData>> {
        protected override FeatureSpec createObject() => new (random<FeatureSpecData>());
        [TestMethod] public void ValueTest() => isReadOnly();
    }
}