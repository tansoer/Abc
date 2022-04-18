using System;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {

    [TestClass]
    public class NamePrefixesRepoTests : PartyRepoTests<NamePrefixesRepo,
        NamePrefix, NamePrefixData> {
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            obj = getObject(db);
        }
        protected override Type getBaseClass()
            => typeof(EntityRepo<NamePrefix, NamePrefixData>);
        protected override NamePrefixesRepo getObject(PartyDb c) => new (c);
        protected override DbSet<NamePrefixData> getSet(PartyDb c) => c.PersonNamePrefixes;
        [TestMethod] public void NextIndexTest() {
            Assert.AreEqual(1, obj.NextIndex(rndStr));
            testGetNext(rndStr);
        }
        private void testGetNext(string masterId) {
            var c = GetRandom.UInt8(10, 20);
            for (byte i = 0; i < c; i++) {
                var e = GetRandom.ObjectOf<NamePrefixData>();
                e.Index = i;
                e.NameId = masterId;
                db.PersonNamePrefixes.Add(e);
                db.SaveChanges();
            }
            Assert.AreEqual(c + 1, obj.NextIndex(masterId));
        }
    }
}