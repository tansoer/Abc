using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class PartyRoleTypeDataTests :SealedTests<PartyRoleTypeData, EntityTypeData> {
        [TestMethod] public void RuleSetIdTest() => isNullable<string>();
    }
}
