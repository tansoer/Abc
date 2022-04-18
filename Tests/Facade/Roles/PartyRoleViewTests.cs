using Abc.Facade.Common;
using Abc.Facade.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Roles {
    [TestClass] public class PartyRoleViewTests : SealedTests<PartyRoleView, PartyAttributeView> {
        [TestMethod] public void PartyRoleTypeIdTest() => isNullableProperty<string>("Role type");
    }
}