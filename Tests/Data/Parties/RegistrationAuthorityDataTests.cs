using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class RegistrationAuthorityDataTests : SealedTests<RegistrationAuthorityData, PartyAttributeData> {

        [TestMethod] public void AuthorityTypeIdTest() => isNullable<string>();
    }
}