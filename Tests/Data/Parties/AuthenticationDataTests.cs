using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class AuthenticationDataTests : SealedTests<AuthenticationData, PartyAttributeData> {

        [TestMethod] public void PartyUserIdTest() => isNullable<string>();

        [TestMethod] public void TokenTest() => isNullable<string>();

        [TestMethod] public void AuthenticationProviderTest() => isNullable<string>();
    }

}
