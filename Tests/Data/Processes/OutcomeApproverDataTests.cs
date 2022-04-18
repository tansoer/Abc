using Abc.Data.Common;
using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class OutcomeApproverDataTests :SealedTests<OutcomeApproverData, EntityBaseData> {
        [TestMethod] public void OutcomeIdTest() => isNullable<string>();
        [TestMethod] public void ApproverSignatureIdTest() => isNullable<string>();
    }
}
