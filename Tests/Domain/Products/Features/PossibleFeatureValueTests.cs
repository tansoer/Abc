using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products.Features {

    [TestClass]
    public class PossibleFeatureValueTests : SealedTests<PossibleFeatureValue, FeatureValue<PossibleFeatureValueData>> {
        protected override PossibleFeatureValue createObject() => new PossibleFeatureValue(GetRandom.ObjectOf<PossibleFeatureValueData>());
        [TestMethod] public void RelationTest() => isReadOnly(obj.Data.Relation);
    }
}