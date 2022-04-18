using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {

    [TestClass] public class ProcessViewTests : SealedTests<ProcessView, EntityBaseView> {
        [TestMethod] public void RuleContextIdTest() => isNullableProperty<string>("Rule Context");
        [TestMethod] public void ProcessTypeIdTest() => isNullableProperty<string>("Process Type");
        [TestMethod] public void ManagerPartyRoleIdTest() => isNullableProperty<string>("Manager");
        [TestMethod] public void InitiatorPartyRoleIdTest() => isNullableProperty<string>("Initiator");
        [TestMethod] public void PriorityClassifierIdTest() => isNullableProperty<string>("Priority");

    }

}