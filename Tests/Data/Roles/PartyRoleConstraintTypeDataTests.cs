using Abc.Aids.Enums;
using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class PartyRoleConstraintTypeDataTests :SealedTests<PartyRoleConstraintTypeData, EntityBaseData>{
        [TestMethod] public void PartyTypeTest() => isProperty<PartyType>();
        [TestMethod] public void OrganizationTypeIdTest() => isNullable<string>();
    }
}
