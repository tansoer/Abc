using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class RegisteredIdentifierViewTests : SealedTests<RegisteredIdentifierView, PartyAttributeView> {
        [TestMethod] public void RegisteredIdentifierTypeIdTest() => isProperty<string>("Registered identifier type");
        [TestMethod] public void RegistrationAuthorityIdTest() => isProperty<string>("Registration authority");
        [TestMethod] public void NameTest() => isProperty<string>("Identifier", true);
        [TestMethod] public void DetailsTest() => isProperty<string>("Comments");
    }
}
