using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Attributes {

    [TestClass] public class BasePartyAttributeTests:
        AbstractTests<BasePartyAttribute<PartyNameData>,
        Entity<PartyNameData>> {
        private class testClass :BasePartyAttribute<PartyNameData> {
            public testClass(PartyNameData d = null) : base(d) { }
        }
        protected override BasePartyAttribute<PartyNameData> createObject() =>
            new testClass(GetRandom.ObjectOf<PartyNameData>());
        [TestMethod] public void PartyIdTest() => isReadOnly(obj.Data.PartyId);

        [TestMethod]
        public async Task PartyTest()
            => await testItemAsync<PartyData, IParty, IPartiesRepo>(
                obj.PartyId, () => obj.Party.Data, PartyFactory.Create);
    }
}
