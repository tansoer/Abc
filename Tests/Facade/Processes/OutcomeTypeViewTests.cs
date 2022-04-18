using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class OutcomeTypeViewTests :SealedTests<OutcomeTypeView, ProcessElementTypeView> {
        [TestMethod] public void ActionTypeIdTest() => isNullableProperty<string>("Action type");
    }
}