using Abc.Aids.Random;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Common {
    [TestClass] public class EntityTypeTests
        :AbstractTests<EntityType<IPartyRoleTypesRepo, PartyRoleType, PartyRoleTypeData>,
            Entity<PartyRoleTypeData>> {
        private class testClass :EntityType<IPartyRoleTypesRepo, PartyRoleType, PartyRoleTypeData> {
            public testClass(PartyRoleTypeData d = null) :base(d) { }
        }

        protected override EntityType<IPartyRoleTypesRepo, PartyRoleType, PartyRoleTypeData> createObject() =>
            new testClass(GetRandom.ObjectOf<PartyRoleTypeData>());

        [TestMethod] public async Task BaseTypeTest()
            => await testItemAsync<PartyRoleTypeData, PartyRoleType, IPartyRoleTypesRepo>(
                obj.baseTypeId, () => obj.BaseType.Data, d => new PartyRoleType(d));
    }
}