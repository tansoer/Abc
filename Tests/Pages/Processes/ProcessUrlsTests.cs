using Abc.Pages.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Pages.Processes {
    [TestClass]
    public class ProcessUrlsTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(ProcessUrls);
        [TestMethod] public void ProcessesTest() =>
            areEqual("/Processes/CRM", ProcessUrls.Processes);
        [TestMethod] public void ProcessTypesTest() =>
            areEqual("/Processes/ProcessTypes", ProcessUrls.ProcessTypes);
        [TestMethod] public void OutcomesTest() =>
            areEqual("/Processes/Outcomes", ProcessUrls.Outcomes);
        [TestMethod] public void OutcomeTypesTest() =>
            areEqual("/Processes/OutcomeTypes", ProcessUrls.OutcomeTypes);
        [TestMethod] public void TasksTest() =>
            areEqual("/Processes/Tasks", ProcessUrls.Tasks);
        [TestMethod] public void TaskParticipantsTest() =>
            areEqual("/Processes/TaskParticipants", ProcessUrls.TaskParticipants);
        [TestMethod] public void TaskTypesTest() =>
            areEqual("/Processes/TaskTypes", ProcessUrls.TaskTypes);
        [TestMethod] public void ActionsTest() =>
            areEqual("/Processes/Actions", ProcessUrls.Actions);
        [TestMethod] public void ActionTypesTest() =>
            areEqual("/Processes/ActionTypes", ProcessUrls.ActionTypes);
        [TestMethod] public void ThreadsTest() =>
            areEqual("/Processes/Threads", ProcessUrls.Threads);
        [TestMethod] public void ThreadTypesTest() =>
            areEqual("/Processes/ThreadTypes", ProcessUrls.ThreadTypes);
        [TestMethod] public void AttributeValuesTest() =>
            areEqual("/Processes/AttributeValues", ProcessUrls.AttributeValues);
        [TestMethod] public void AttributeTypesTest() =>
            areEqual("/Processes/AttributeTypes", ProcessUrls.AttributeTypes);
        [TestMethod] public void ActionApproversTest() =>
            areEqual("/Processes/ActionApprovers", ProcessUrls.ActionApprovers);
        [TestMethod] public void OutcomeApproversTest() =>
            areEqual("/Processes/OutcomeApprovers", ProcessUrls.OutcomeApprovers);
        [TestMethod] public void MarketingTest() =>
            areEqual("/Processes/Marketing", ProcessUrls.Marketing);
        [TestMethod] public void RecruitingTest() =>
            areEqual("/Processes/Recruiting", ProcessUrls.Recruiting);
    }
}
