using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
    [TestClass] public class EntityBaseViewTests: 
        AbstractTests<EntityBaseView, DetailedView> {
        private class testClass : EntityBaseView { }
        protected override EntityBaseView createObject() => random<testClass>();
        [TestMethod] public void NameTest() => isNullable<string>();
        [TestMethod] public void CodeTest() => isNullable<string>();
        [TestMethod] public void DetailsTest() => isNullable<string>();
    }
}