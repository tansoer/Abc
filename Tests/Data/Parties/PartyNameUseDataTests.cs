using Abc.Data.Common;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass]
    public class PartyNameUseDataTests : SealedTests<PartyNameUseData, EntityBaseData> {

        [TestMethod] public void NameUseTypeIdTest() => isNullable<string>();

        [TestMethod] public void PartyNameIdTest() => isNullable<string>();
    }

}
