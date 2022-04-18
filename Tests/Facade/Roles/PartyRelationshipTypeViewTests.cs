using Abc.Facade.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Roles {
    [TestClass] public class PartyRelationshipTypeViewTests
        :SealedTests<PartyRelationshipTypeView, PartyRelationshipBaseTypeView> { }
}