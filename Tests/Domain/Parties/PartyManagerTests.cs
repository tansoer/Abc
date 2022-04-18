using System.Linq;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Data.Roles;
using Abc.Domain.Common;
using Abc.Domain.Parties;
using Abc.Domain.Parties.Identifiers;
using Abc.Domain.Parties.Names;
using Abc.Domain.Roles;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties {
    [TestClass]
    public class PartyManagerTests: SealedTests<PartyManager, Manager<IPartiesRepo, IParty>> {
        private Person o;
        protected override PartyManager createObject() {
            o = new Person(GetRandom.ObjectOf<PartyData>());
            GetRepo.Instance<IPartiesRepo>().Add(o);
            return new PartyManager();
        }

        [TestMethod]
        public void FindByNameTest() {
            var name = new PersonName(new PartyNameData { PartyId = o.Id });
            allPropertiesAreEqual(o.Data, obj.FindByName(name).Data);
        }

        [TestMethod]
        public void FindByNameStringTest() {
            var name = new PersonName(GetRandom.ObjectOf<PartyNameData>());
            GetRepo.Instance<IPartyNamesRepo>().Add(name);
            isTrue(obj.FindByName(name.Name).Any(x => x.Name == name.Name));
        }

        [TestMethod]
        public void FindByRegisteredIdTest() {
            var id = new RegisteredIdentifier(new RegisteredIdentifierData{PartyId = o.Id});
            allPropertiesAreEqual(o.Data, obj.FindByRegisteredId(id).Data);
        }

        [TestMethod]
        public void FindByRegisteredIdStringTest() {
            var id = new RegisteredIdentifier(GetRandom.ObjectOf<RegisteredIdentifierData>());
            GetRepo.Instance<IRegisteredIdentifiersRepo>().Add(id);
            isTrue(obj.FindByRegisteredId(id.Identifier).Any(x => x.Identifier == id.Identifier));
        }

        [TestMethod]
        public void FindBySummaryTest() {
            var s = new PartySummary(new PartySummaryData { PartyId = o.Id });
            allPropertiesAreEqual(o.Data, obj.FindBySummary(s).Data);
        }

        [TestMethod]
        public void FindBySummaryStringTest() {
            var s = new PartySummary(GetRandom.ObjectOf<PartySummaryData>());
            GetRepo.Instance<IPartySummariesRepo>().Add(s);
            isTrue(obj.FindBySummary(s.Name).Any(x => x.Name == s.Name));
        }

        [TestMethod]
        public void FindByRoleTest() {
            var r = new PartyRole(new PartyRoleData { PartyId = o.Id });
            allPropertiesAreEqual(o.Data, obj.FindByRole(r).Data);
        }
    }
}
