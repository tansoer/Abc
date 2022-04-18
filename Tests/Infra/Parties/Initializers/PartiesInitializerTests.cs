using Abc.Infra.Parties;
using Abc.Infra.Parties.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Parties.Initializers {

    [TestClass] public class PartiesInitializerTests :  DbInitializerTests<PartyDb> {
        public PartiesInitializerTests() {
            type = typeof(PartiesInitializer);
            db = new PartyDb(options);
            removeAll(db.Parties);
            removeAll(db.PartyContacts);
            removeAll(db.PartyNames);
            removeAll(db.PersonNamePrefixes);
            PartiesInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void PartiesTest() => areEqual(5, db.Parties.Count());
        [TestMethod] public void PartyContactsTest() => areEqual(15, db.PartyContacts.Count());
        [TestMethod] public void PartyNamesTest() => areEqual(5, db.PartyNames.Count());
        [TestMethod] public void PersonNamePrefixesTest() => areEqual(3, db.PersonNamePrefixes.Count());
    }
}
