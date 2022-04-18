using Abc.Data.Parties;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class PartyCapabilityDataTests : SealedTests<PartyCapabilityData, PartyAttributeData> {
        [TestMethod] public void RuleContextIdTest() => isNullable<string>();
    }
}
