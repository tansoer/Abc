using Abc.Facade.Processes;
using Abc.Facade.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class TaskTypeViewTests :SealedTests<TaskTypeView, PartyRelationshipBaseTypeView> {
        [TestMethod] public void ThreadTypeIdTest() => isNullableProperty<string>("Thread type");
        [TestMethod] public void NextElementIdTest() => isNullableProperty<string>("Next element");
        [TestMethod] public void PreviousElementIdTest() => isNullableProperty<string>("Previous element");
    }
}