using Abc.Facade.Common;
using Abc.Facade.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Roles {
    [TestClass] public class PartyRoleTypeViewTests :SealedTests<PartyRoleTypeView, EntityTypeView> {
        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule set");
    }
}