using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class ResponsibilityTypeDataTests :SealedTests<ResponsibilityTypeData, EntityTypeData>{
        [TestMethod] public void RequirementsRuleSetIdTest() => isNullable<string>();
        [TestMethod] public void ConditionsOfSatisfactionRuleSetIdTest() => isNullable<string>();
    }
}
