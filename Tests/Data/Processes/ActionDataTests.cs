using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class ActionDataTests :SealedTests<ActionData, ProcessElementData> {
        [TestMethod] public void ActionTypeIdTest() => isNullable<string>();
        [TestMethod] public void TaskIdTest() => isNullable<string>();
        [TestMethod] public void InitiatorSignatureIdTest() => isNullable<string>();
        [TestMethod] public void ActionStatusClassifierIdTest() => isNullable<string>();

    }
}
