using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class PartyRoleConstraintDataTests :SealedTests<PartyRoleConstraintData, EntityBaseData>{
        [TestMethod] public void RoleConstraintTypeIdTest() => isNullable<string>();
        [TestMethod] public void PartyRoleTypeIdTest() => isNullable<string>();
    }
}
