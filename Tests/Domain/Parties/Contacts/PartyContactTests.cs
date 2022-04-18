using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Common;
using Abc.Domain.Parties.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Contacts {
    [TestClass] public class PartyContactTests :AbstractTests<PartyContact<PartyContactData>,
        Entity<PartyContactData>> {
        private class testClass :PartyContact<PartyContactData> {
            public testClass(PartyContactData d) : base(d) { }
        }
        protected override PartyContact<PartyContactData> createObject() =>
            new testClass(GetRandom.ObjectOf<PartyContactData>());
    }
}
