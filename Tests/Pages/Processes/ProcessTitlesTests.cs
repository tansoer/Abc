using Abc.Pages.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class ProcessTitlesTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ProcessTitles);
        [TestMethod] public void ProcessesTest() => areEqual("Processes", ProcessTitles.Processes);
        [TestMethod] public void ProcessTypesTest() => areEqual("Process types", ProcessTitles.ProcessTypes);
        [TestMethod] public void OutcomesTest() => areEqual("Outcomes", ProcessTitles.Outcomes);
        [TestMethod] public void OutcomeTypesTest() => areEqual("Outcome types", ProcessTitles.OutcomeTypes);
        [TestMethod] public void TasksTest() => areEqual("Tasks", ProcessTitles.Tasks);
        [TestMethod] public void TaskParticipantsTest() => areEqual("Task participants", ProcessTitles.TaskParticipants);
        [TestMethod] public void TaskTypesTest() => areEqual("Task types", ProcessTitles.TaskTypes);
        [TestMethod] public void ActionsTest() => areEqual("Actions", ProcessTitles.Actions);
        [TestMethod] public void ActionTypesTest() => areEqual("Action types", ProcessTitles.ActionTypes);
        [TestMethod] public void ThreadsTest() => areEqual("Threads", ProcessTitles.Threads);
        [TestMethod] public void ThreadTypesTest() => areEqual("Thread types", ProcessTitles.ThreadTypes);
        [TestMethod] public void AttributeValuesTest() => areEqual("Attribute values", ProcessTitles.AttributeValues);
        [TestMethod] public void AttributeTypesTest() => areEqual("Attribute types", ProcessTitles.AttributeTypes);
        [TestMethod] public void ActionApproversTest() => areEqual("Action approvers", ProcessTitles.ActionApprovers);
        [TestMethod] public void OutcomeApproversTest() => areEqual("Outcome approvers", ProcessTitles.OutcomeApprovers);
        [TestMethod] public void MarketingTest() => areEqual("Marketing", ProcessTitles.Marketing);
        [TestMethod] public void RecruitingTest() => areEqual("Recruiting", ProcessTitles.Recruiting);
    }
}
