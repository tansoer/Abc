using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Common {
    [TestClass] public class ProcessElementBaseViewTests :AbstractTests<ProcessElementBaseView, EntityBaseView> {
        private class testClass :ProcessElementBaseView { }
        protected override ProcessElementBaseView createObject() => random<testClass>();
        [TestMethod] public void NextElementIdTest() => isNullableProperty<string>("Next element");
        [TestMethod] public void PreviousElementIdTest() => isNullableProperty<string>("Previous element");
    }
}