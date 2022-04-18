using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Signatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Signatures {
    [TestClass]
    public class AuthenticationTests : SealedTests<Authentication, BasePartyAttribute<AuthenticationData>> {
        protected override Authentication createObject()
            => new Authentication(GetRandom.ObjectOf<AuthenticationData>());
        [TestMethod] public void PartyUserIdTest() => isReadOnly(obj.Data.PartyUserId);
        [TestMethod] public void TokenTest() => isReadOnly(obj.Data.Token);
        [TestMethod] public void AuthenticationProviderTest() => isReadOnly(obj.Data.AuthenticationProvider);
    }
}
