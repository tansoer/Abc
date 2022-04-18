using Abc.Facade.Common;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Parties {
    [TestClass] public class NameAttributeViewTests :
        AbstractTests<NameAttributeView, PartyAttributeView> {
        private class testClass :NameAttributeView { }
        protected override NameAttributeView createObject() => random<testClass>();
        [TestMethod] public void NameIdTest() => isNullableProperty<string>("Name", true);
        [TestMethod] public void IndexTest() => isProperty<byte>("Index", true);
    }
}
