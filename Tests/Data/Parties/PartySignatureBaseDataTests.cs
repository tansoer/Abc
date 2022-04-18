using Abc.Aids.Random;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass] public class PartySignatureBaseDataTests : AbstractTests<PartySignatureBaseData, PartyAttributeData> {
        private class testClass : PartySignatureBaseData { }
        protected override PartySignatureBaseData createObject() => GetRandom.ObjectOf<testClass>();
        [TestMethod] public void PartyAuthenticationIdTest() => isNullable<string>();
        [TestMethod] public void PartySummaryIdTest() => isNullable<string>();
        [TestMethod] public void SignedEntityIdTest() => isNullable<string>();
        [TestMethod] public void SignedEntityTypeIdTest() => isNullable<string>();
    }
}