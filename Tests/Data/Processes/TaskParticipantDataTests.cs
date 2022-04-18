using Abc.Data.Common;
using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class TaskParticipantDataTests :SealedTests<TaskParticipantData, EntityBaseData> {
        [TestMethod] public void PartyRoleIdTest() => isNullable<string>();
        [TestMethod] public void TaskIdTest() => isNullable<string>();
    }
}
