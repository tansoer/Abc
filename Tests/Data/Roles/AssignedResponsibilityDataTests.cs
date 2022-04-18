using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class AssignedResponsibilityDataTests : SealedTests<AssignedResponsibilityData, EntityBaseData>{
        [TestMethod] public void PartyRoleIdTest() => isNullable<string>();
        [TestMethod] public void ResponsibilityIdTest() => isNullable<string>();
        [TestMethod] public void PartySignatureIdTest() => isNullable<string>();
    }
}
