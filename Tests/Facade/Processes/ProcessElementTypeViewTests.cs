using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ProcessElementTypeViewTests :AbstractTests<ProcessElementTypeView, ProcessElementBaseView> {
        private class testClass:ProcessElementTypeView { }

        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule set");
        [TestMethod] public void BaseTypeIdTest() => isNullableProperty<string>("Base type");

        protected override ProcessElementTypeView createObject() => random<testClass>();
    }
}