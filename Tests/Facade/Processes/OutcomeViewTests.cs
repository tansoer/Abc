using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class OutcomeViewTests :SealedTests<OutcomeView, ProcessElementView> {
        [TestMethod] public void OutcomeTypeIdTest() => isNullableProperty<string>("Outcome type");
        [TestMethod] public void ActionIdTest() => isNullableProperty<string>("Action");
        [TestMethod] public void IsPossibleOutcomeTest() => isProperty<bool>("Is possible outcome");
    }
}