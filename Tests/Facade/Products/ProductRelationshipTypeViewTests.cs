using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class ProductRelationshipTypeViewTests :SealedTests<ProductRelationshipTypeView, EntityTypeView> {
        [TestMethod] public void ConsumerTypeIdTest() => isNullableProperty<string>("Consumer Type");
        [TestMethod] public void ProviderTypeIdTest() => isNullableProperty<string>("Provider Type");
    }
}
