using Abc.Aids.Enums;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Abc.Tests.Facade.Products {
    [TestClass] public class PossibleFeatureValueViewTests 
        : SealedTests<PossibleFeatureValueView, FeatureValueView> {
        [TestMethod] public void RelationTest() => isProperty<Relation>("Relation");
    }
}
