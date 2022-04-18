using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class RegisteredIdentifierDataTests :SealedTests<RegisteredIdentifierData, PartyAttributeData> {
        [TestMethod] public void RegisteredIdentifierTypeIdTest() => isNullable<string>();
        [TestMethod] public void RegistrationAuthorityIdTest() => isNullable<string>();
    }
}
