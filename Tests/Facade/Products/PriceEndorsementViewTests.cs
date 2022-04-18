using Abc.Facade.Common;
using Abc.Facade.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Products {
    [TestClass]
    public class PriceEndorsementViewTests :SealedTests<PriceEndorsementView, EntityBaseView> {
        [TestMethod] public void PriceIdTest() => isNullableProperty<string>("Price");
        [TestMethod] public void PartySignatureIdTest() => isNullableProperty<string>("Party Signature");
    }
}
