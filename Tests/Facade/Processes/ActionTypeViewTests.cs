using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ActionTypeViewTests :SealedTests<ActionTypeView, ProcessElementTypeView> {
        [TestMethod] public void TaskTypeIdTest() => isNullableProperty<string>("Task type");
    }
}