using Abc.Aids.Enums;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {

    [TestClass]
    public class PartyViewTests : SealedTests<PartyView, EntityBaseView> {
        [TestMethod] public void DescriptionTest() => isNullableProperty<string>("Description");

        [TestMethod] public void GenderTest() => isProperty<IsoGender>("Gender");

        [TestMethod] public void NameTest() => isProperty<string>("Primary Name");

        [TestMethod] public void PartyTypeTest() => isProperty<PartyType>("Party Type");
        [TestMethod] public void OrganizationTypeIdTest() => isProperty<string>("Organization Type");

        [DataRow(PartyType.Person, true)]
        [DataRow(PartyType.Organization, false)]
        [DataTestMethod]
        public void IsPersonTest(PartyType t, bool expected) {
            obj.PartyType = t;
            areEqual(expected, obj.IsPerson());
        }

        [DataRow(PartyType.Person, false)]
        [DataRow(PartyType.Organization, true)]
        [DataTestMethod]
        public void IsOrganizationTest(PartyType t, bool expected) {
            obj.PartyType = t;
            areEqual(expected, obj.IsOrganization());
        }

        [TestMethod]
        public void ToStringTest()
            => Assert.AreEqual(new PartyViewFactory().Create(obj).ToString(), obj.ToString());
    }
}
