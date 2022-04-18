using Abc.Aids.Random;
using Abc.Data.Common;
using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {

    [TestClass] public class PartyAttributeDataTests: 
        AbstractTests<PartyAttributeData, EntityBaseData> {

        private class testClass : PartyAttributeData { }
        protected override PartyAttributeData createObject() => GetRandom.ObjectOf<testClass>();

        [TestMethod] public void PartyIdTest() => isNullable<string>();
    }
}
