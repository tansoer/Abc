using Abc.Data.Common;
using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class ActionApproverDataTests :SealedTests<ActionApproverData, EntityBaseData> {
        [TestMethod] public void ActionIdTest() => isNullable<string>();
        [TestMethod] public void ApproverSignatureIdTest() => isNullable<string>();
    }
}
