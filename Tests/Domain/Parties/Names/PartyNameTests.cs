using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Attributes;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Names {
    [TestClass] public class PartyNameTests :AbstractTests<PartyName,
        BasePartyAttribute<PartyNameData>> {
        private class testClass :PartyName {
            public testClass(PartyNameData d) :base(d) { }
        }

        protected override PartyName createObject() =>
            new testClass(GetRandom.ObjectOf<PartyNameData>());

        [TestMethod] public void NameTest() => isReadOnly(obj.Data.Name);
        [TestMethod] public void NameTypeTest() => isReadOnly(obj.Data.NameType);

        [TestMethod] public async Task NameUseTest()
            => await testListAsync<PartyNameUse, PartyNameUseData, IPartyNameUsesRepo>(
                d => d.PartyNameId = obj.Id,
                d => new PartyNameUse(d));
    }
}

