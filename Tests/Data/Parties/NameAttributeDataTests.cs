using Abc.Data.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Parties {
    [TestClass] public class NameAttributeDataTests :AbstractTests<NameAttributeData, PartyAttributeData> {
        private class testClass:NameAttributeData { }
        [TestMethod] public void NameIdTest() => isNullable<string>();
        [TestMethod] public void IndexTest() => isProperty<byte>();
        protected override NameAttributeData createObject() => random<testClass>();
    }
}

