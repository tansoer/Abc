using System;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Abc.Infra.Common;
using Abc.Infra.Parties;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Parties {
    [TestClass] public class NameSuffixesRepoTests : PartyRepoTests<NameSuffixesRepo,
        NameSuffix, NameSuffixData> {
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            obj = getObject(db);
        }
        protected override Type getBaseClass()
            => typeof(EntityRepo<NameSuffix, NameSuffixData>);
        protected override NameSuffixesRepo getObject(PartyDb c) => new (c);
        protected override DbSet<NameSuffixData> getSet(PartyDb c) => c.PersonNameSuffixes;
        [TestMethod] public void NextIndexTest() {
            Assert.AreEqual(1, obj.NextIndex(rndStr));
            testGetNext(rndStr);
        }
        private void testGetNext(string masterId) {
            var c = GetRandom.UInt8(10, 20);
            for (byte i = 0; i < c; i++) {
                var e = GetRandom.ObjectOf<NameSuffixData>();
                e.Index = i;
                e.NameId = masterId;
                db.PersonNameSuffixes.Add(e);
                db.SaveChanges();
            }
            Assert.AreEqual(c + 1, obj.NextIndex(masterId));
        }
    }
}