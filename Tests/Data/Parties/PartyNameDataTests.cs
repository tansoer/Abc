using Abc.Aids.Enums;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class PartyNameDataTests :SealedTests<PartyNameData, PartyAttributeData> {
        [TestMethod] public void NameTypeTest() => isProperty<NameType>();
        [TestMethod] public void NameTest() => isNullable<string>();
        [TestMethod] public void GivenNameTest() => isNullable<string>();
        [TestMethod] public void MiddleNameTest() => isNullable<string>();
        [TestMethod] public void PreferredNameTest() => isNullable<string>();
        [TestMethod] public void PartyTypeTest() => isProperty<PartyType>();
    }
}
