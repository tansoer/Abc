using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class OutcomeDataTests :SealedTests<OutcomeData, ProcessElementData> {
        [TestMethod] public void OutcomeTypeIdTest() => isNullable<string>();
        [TestMethod] public void ActionIdTest() => isNullable<string>();
        [TestMethod] public void IsPossibleOutcomeTest() => isProperty<bool>();
    }
}
