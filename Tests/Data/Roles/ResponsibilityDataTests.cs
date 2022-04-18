using Abc.Data.Common;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Roles {
    [TestClass]
    public class ResponsibilityDataTests :SealedTests<ResponsibilityData, EntityBaseData> {
        [TestMethod] public void IsOptionalTest() => isProperty<bool>();
        [TestMethod] public void ResponsibilityTypeIdTest() => isProperty<string>();
        [TestMethod] public void PartyRoleTypeIdTest() => isProperty<string>();
    }
}
