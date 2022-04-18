using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class ActionTypeDataTests :SealedTests<ActionTypeData, ProcessElementTypeData> {
        [TestMethod] public void TaskTypeIdTest() => isNullable<string>();
    }
}
