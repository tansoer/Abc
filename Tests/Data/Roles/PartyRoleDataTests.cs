using Abc.Data.Parties;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {

    [TestClass] public class PartyRoleDataTests : SealedTests<PartyRoleData, PartyAttributeData> {
        [TestMethod] public void PartyRoleTypeIdTest() => isNullable<string>();
    }

}