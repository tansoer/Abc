using Abc.Aids.Random;
using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {

    [TestClass] public class PartySignatureBaseViewTests: 
        AbstractTests<PartySignatureBaseView, PartyAttributeView> {
        private class testClass : PartySignatureBaseView { }
        protected override PartySignatureBaseView createObject() => random<testClass>();
        [TestMethod] public void PartyAuthenticationIdTest() => isNullableProperty<string>("Party authentication");
        [TestMethod] public void SignedEntityIdTest() => isNullableProperty<string>("Signed entity");
        [TestMethod] public void SignedEntityTypeIdTest() => isNullableProperty<string>("Signed entity type");
        [TestMethod] public void PartySummaryIdTest() => isNullableProperty<string>("Party summary");
    }
}
