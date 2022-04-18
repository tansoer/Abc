using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ActionViewTests :SealedTests<ActionView, ProcessElementView> {
        [TestMethod] public void ActionTypeIdTest() => isNullableProperty<string>("Action type");
        [TestMethod] public void TaskIdTest() => isNullableProperty<string>("Task");
        [TestMethod] public void InitiatorSignatureIdTest() => isNullableProperty<string>("Initiator signature");
        [TestMethod] public void ActionStatusClassifierIdTest() => isNullableProperty<string>("Action status classifier");
    }
}