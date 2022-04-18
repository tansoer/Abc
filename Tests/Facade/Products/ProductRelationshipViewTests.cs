using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class ProductRelationshipViewTests :SealedTests<ProductRelationshipView, EntityBaseView> {
        [TestMethod] public void RelationshipTypeIdTest() => isNullableProperty<string>("Relationship Type");
        [TestMethod] public void ConsumerEntityIdTest() => isNullableProperty<string>("Consumer Entity");
        [TestMethod] public void ProviderEntityIdTest() => isNullableProperty<string>("Provider Entity");

    }
}
