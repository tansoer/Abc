using Abc.Data.Processes;
using Abc.Data.Roles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class TaskTypeDataTests :SealedTests<TaskTypeData, PartyRelationshipBaseTypeData> {
        [TestMethod] public void ThreadTypeIdTest() => isNullable<string>();
        [TestMethod] public void NextElementIdTest() => isNullable<string>();
        [TestMethod] public void PreviousElementIdTest() => isNullable<string>();
    }
}
