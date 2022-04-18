using Abc.Data.Processes;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class TaskDataTests :SealedTests<TaskData, PartyRelationshipBaseData> {
        [TestMethod] public void ThreadIdTest() => isNullable<string>();
        [TestMethod] public void RuleContextIdTest() => isNullable<string>();
        [TestMethod] public void NextElementIdTest() => isNullable<string>();
        [TestMethod] public void PreviousElementIdTest() => isNullable<string>();
        [TestMethod] public void FromPartyAddressIdTest() => isNullable<string>();
        [TestMethod] public void ToPartyAddressIdTest() => isNullable<string>();
        [TestMethod] public void IsEscalationTest() => isProperty<bool>();
        protected override TaskData createObject() => random<TaskData>();
    }
}
