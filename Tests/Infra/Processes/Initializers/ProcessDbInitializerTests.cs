using Abc.Infra.Processes;
using Abc.Infra.Processes.Initializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Abc.Tests.Infra.Processes.Initializers {
    [TestClass] public class ProcessDbInitializerTests :DbInitializerTests<ProcessDb> {
        public ProcessDbInitializerTests() {
            type = typeof(ProcessDbInitializer);
            db = new ProcessDb(options);
            ProcessDbInitializer.Initialize(db);
        }
        [TestMethod] public void InitializeTest() { }
        [TestMethod] public void AttributeValuesTest() => areEqual(0, db.AttributeValues.Count());
        [TestMethod] public void ActionApproversTest() => areEqual(0, db.ActionApprovers.Count());
        [TestMethod] public void ActionTypesTest() => areEqual(37, db.ActionTypes.Count());
        [TestMethod] public void ActionsTest() => areEqual(0, db.Actions.Count());
        [TestMethod] public void OutcomeApproversTest() => areEqual(0, db.OutcomeApprovers.Count());
        [TestMethod] public void OutcomeTypesTest() => areEqual(37, db.OutcomeTypes.Count());
        [TestMethod] public void ProcessTypesTest() => areEqual(1, db.ProcessTypes.Count());
        [TestMethod] public void ProcessesTest() => areEqual(0, db.Processes.Count());
        [TestMethod] public void TaskTypesTest() => areEqual(22, db.TaskTypes.Count());
        [TestMethod] public void ThreadsTest() => areEqual(0, db.Threads.Count());
        [TestMethod] public void TasksTest() => areEqual(0, db.Tasks.Count());
        [TestMethod] public void OutcomesTest() => areEqual(0, db.Outcomes.Count());
        [TestMethod] public void AttributeTypesTest() => areEqual(0, db.AttributeTypes.Count());
        [TestMethod] public void ThreadTypesTest() => areEqual(4, db.ThreadTypes.Count());
        [TestMethod] public void TaskParticipantsTest() => areEqual(0, db.TaskParticipants.Count());
    }
}