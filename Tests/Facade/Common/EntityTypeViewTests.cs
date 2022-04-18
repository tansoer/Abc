using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {

    [TestClass] public class EntityTypeViewTests: 
        AbstractTests<EntityTypeView, EntityBaseView> {
        private class testClass : EntityTypeView { }
        protected override EntityTypeView createObject() => new testClass();
        [TestMethod] public void BaseTypeIdTest() => isNullable<string>();
    }
}
