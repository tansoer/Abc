using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ProcessTypeViewTests :SealedTests<ProcessTypeView, EntityTypeView> {
        [TestMethod] public void RuleSetIdTest() => isNullableProperty<string>("Rule Set");
    }
}
