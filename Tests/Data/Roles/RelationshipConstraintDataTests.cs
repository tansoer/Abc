using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class RelationshipConstraintDataTests : SealedTests<RelationshipConstraintData, EntityBaseData> {
        [TestMethod] public void ConstraintTypeIdTest() => isNullable<string>();
        [TestMethod] public void RelationshipTypeIdTest() => isNullable<string>();
    }
}
