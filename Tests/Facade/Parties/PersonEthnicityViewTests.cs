using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass] public class PersonEthnicityViewTests: 
        SealedTests<PersonEthnicityView, PartyAttributeView> {
        [TestMethod] public void EthnicityIdTest() => isNullableProperty<string>("Ethnicity");
    }
}
