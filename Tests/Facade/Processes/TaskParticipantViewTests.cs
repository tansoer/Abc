using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class TaskParticipantViewTests :SealedTests<TaskParticipantView, EntityBaseView> {
        [TestMethod] public void PartyRoleIdTest() => isNullableProperty<string>("Party Role");
        [TestMethod] public void TaskIdTest() => isNullableProperty<string>("Task");
    }
}