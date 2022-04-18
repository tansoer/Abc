using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class PersonEthnicityDataTests : SealedTests<PersonEthnicityData, PartyAttributeData> {

        [TestMethod] public void EthnicityIdTest() => isNullable<string>();
    }
}
