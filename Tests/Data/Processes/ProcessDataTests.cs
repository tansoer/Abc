using Abc.Data.Common;
using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {

    [TestClass]
    public class ProcessDataTests : SealedTests<ProcessData, EntityBaseData> {
        [TestMethod] public void ProcessTypeIdTest() => isNullable<string>();
        [TestMethod] public void ManagerPartyRoleIdTest() => isNullable<string>();
        [TestMethod] public void InitiatorPartyRoleIdTest() => isNullable<string>();
        [TestMethod] public void PriorityClassifierIdTest() => isNullable<string>();
        [TestMethod] public void RuleContextIdTest() => isNullable<string>();
    }

}