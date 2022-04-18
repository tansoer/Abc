using Abc.Facade.Processes;
using Abc.Facade.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass]
    public class TaskViewTests :SealedTests<TaskView, PartyRelationshipBaseView> {
        [TestMethod] public void ThreadIdTest() => isNullableProperty<string>("Thread");
        [TestMethod] public void RuleContextIdTest() => isNullableProperty<string>("Rule context");
        [TestMethod] public void NextElementIdTest() => isNullableProperty<string>("Next element");
        [TestMethod] public void PreviousElementIdTest() => isNullableProperty<string>("Previous element");
        [TestMethod] public void FromPartyAddressIdTest() => isNullableProperty<string>("From party");
        [TestMethod] public void ToPartyAddressIdTest() => isNullableProperty<string>("To party");
        [TestMethod] public void IsEscalationTest() => isProperty<bool>("Is escalation");
    }
}
