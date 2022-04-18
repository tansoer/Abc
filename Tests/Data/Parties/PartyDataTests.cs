using Abc.Aids.Enums;
using Abc.Data.Common;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {
    [TestClass] public class PartyDataTests :SealedTests<PartyData, EntityBaseData> {
        [TestMethod] public void OrganizationTypeIdTest() => isNullable<string>();
        [TestMethod] public void GenderTest() => isProperty<IsoGender>();
        [TestMethod] public void PartyTypeTest() => isProperty<PartyType>();
    }
}
