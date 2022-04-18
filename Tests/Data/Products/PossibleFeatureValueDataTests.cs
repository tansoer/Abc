using Abc.Aids.Enums;
using Abc.Data.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Products {

    [TestClass]
    public class PossibleFeatureValueDataTests : SealedTests<PossibleFeatureValueData, FeatureValueData> {

        [TestMethod]
        public void RelationTest() => isProperty<Relation>();

        [TestMethod]
        public void FeatureTypeIdTest() => isProperty<string>();

    }

}