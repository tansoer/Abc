using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Parties.Names {

    [TestClass] public class PersonNameTests :
        SealedTests<PersonName, PartyName> {
        protected override PersonName createObject() {
            base.createObject();
            var d = GetRandom.ObjectOf<PartyNameData>();
            d.PartyType = PartyType.Person;
            return new PersonName(d);
        }

        [TestMethod] public async Task PrefixesTest()
            => await testListAsync<NamePrefix, NamePrefixData, INamePrefixesRepo>(
                d => d.NameId = obj.Id,
                d => new NamePrefix(d));

        [TestMethod] public async Task SuffixesTest()
            => await testListAsync<NameSuffix, NameSuffixData, INameSuffixesRepo>(
                d => d.NameId = obj.Id,
                d => new NameSuffix(d));

        [TestMethod] public void FamilyNameTest() => isReadOnly(obj.Data.Name);
        [TestMethod] public void GivenNameTest() => isReadOnly(obj.Data.GivenName);
        [TestMethod] public void PreferredNameTest() => isReadOnly(obj.Data.PreferredName);
        [TestMethod] public void MiddleNameTest() => isReadOnly(obj.Data.MiddleName);

        [DataRow(null, null, null, null, "Unspecified")]
        [DataRow("name", null, "middle", null, "middle name")]
        [DataRow("name", "given", "middle", "preferred", "preferred name")]
        [DataRow("name", "given", "middle", null, "given middle name")]
        [DataRow("name", "given", null, null, "given name")]
        [DataRow(null, "given", "middle", null, "given middle")]
        [DataTestMethod]
        public void ToStringTest(string name, string given, string middle, string preferred, string expected) {
            var d = new PartyNameData {
                PartyType = PartyType.Person,
                Name = name,
                PreferredName = preferred,
                MiddleName = middle,
                GivenName = given
            };
            obj = new PersonName(d);
            Assert.AreEqual(expected, obj.ToString());
        }
    }
}
