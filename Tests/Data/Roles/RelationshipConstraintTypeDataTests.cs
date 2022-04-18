using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class RelationshipConstraintTypeDataTests :SealedTests<RelationshipConstraintTypeData, EntityBaseData>{
        [TestMethod] public void ConsumerRoleTypeIdTest() => isNullable<string>();
        [TestMethod] public void ProviderRoleTypeIdTest() => isNullable<string>();
    }
}
