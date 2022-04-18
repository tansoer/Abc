using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {

    [TestClass] public class PartyAttributeViewTests: 
        AbstractTests<PartyAttributeView, EntityBaseView> {
        private class testClass :PartyAttributeView {}
        protected override PartyAttributeView createObject() => new testClass();
        [TestMethod] public void PartyIdTest() => isNullableProperty<string>("Party");
        
    }
}
