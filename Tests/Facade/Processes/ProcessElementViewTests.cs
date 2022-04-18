using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ProcessElementViewTests :AbstractTests<ProcessElementView, ProcessElementBaseView> {
        private class testClass :ProcessElementView { }

        [TestMethod] public void RuleContextIdTest() => isNullableProperty<string>("Rule context");
        protected override ProcessElementView createObject() => random<testClass>();
    }
}