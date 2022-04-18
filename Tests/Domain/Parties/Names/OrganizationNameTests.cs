using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Data.Parties;
using Abc.Domain.Parties.Names;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Parties.Names {

    [TestClass]
    public class OrganizationNameTests : SealedTests<OrganizationName, PartyName> {

        protected override OrganizationName createObject() {
            base.createObject();
            var d = GetRandom.ObjectOf<PartyNameData>();
            d.PartyType = PartyType.Organization;
            return new OrganizationName(d);
        }

        [DataRow( "name", "given", "middle", "preferred", "name")]
        [DataRow( "name", "given", "middle", null, "name")]
        [DataRow( "name", "given", null, null, "name")]
        [DataRow( "name", null, "middle", null, "name")]
        [DataRow( null, "given", "middle", null, "Unspecified")]
        [DataRow( null, null, null, null, "Unspecified")]
        [DataTestMethod]

        public void ToStringTest(string name, string given, string middle, string preferred, string expected) {
            var d = new PartyNameData {
                PartyType = PartyType.Organization,
                Name = name,
                PreferredName = preferred,
                MiddleName = middle,
                GivenName = given
            };
            obj = new OrganizationName(d);
            Assert.AreEqual(expected, obj.ToString());
        }
    }
}
