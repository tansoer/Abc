using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass]
    public class PartyNameViewTests : SealedTests<PartyNameView, EntityBaseView> {
        protected override PartyNameView createObject() => GetRandom.ObjectOf<PartyNameView>();
        [TestMethod] public void NameTest() => isNullableProperty<string>("Name", true);
        [TestMethod] public void GivenNameTest() => isNullableProperty<string>("First Name");
        [TestMethod] public void MiddleNameTest() => isNullableProperty<string>("Middle Name");
        [TestMethod] public void PreferredNameTest() => isNullableProperty<string>("Preferred Name");
        [TestMethod] public void PartyTypeTest() => isProperty<PartyType>("Party Type");
        [TestMethod] public void NameTypeTest() => isProperty<NameType>("Name Type");
        [TestMethod] public void PartyIdTest() => isNullable<string>();
        [TestMethod] public void FamilyNameCaptionTest() => areEqual("Family Name",PartyNameView.FamilyNameCaption);
        [DataRow(PartyType.Person, true)]
        [DataRow(PartyType.Organization, false)]
        [DataTestMethod]
        public void IsPersonNameTest(PartyType t, bool expected) {
            obj.PartyType = t;
            areEqual(expected, obj.IsPersonName());
        }
        [DataRow(PartyType.Person, false)]
        [DataRow(PartyType.Organization, true)]
        [DataTestMethod]
        public void IsOrganizationNameTest(PartyType t, bool expected) {
            obj.PartyType = t;
            areEqual(expected, obj.IsOrganizationName());
        }
        [TestMethod] public void ToStringTest() 
            => areEqual(new PartyNameViewFactory().Create(obj).ToString(), obj.ToString());
    }
}
