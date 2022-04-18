using Abc.Data.Roles;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Roles {
    [TestClass]
    public class PartyRelationshipTypeTests : SealedTests<PartyRelationshipType, PartyRelationshipBaseType<PartyRelationshipTypeData>> { }
}
