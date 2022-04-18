using Abc.Facade.Common;
using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ThreadTypeViewTests :SealedTests<ThreadTypeView, ProcessElementTypeView> {
        [TestMethod] public void ProcessTypeIdTest() => isNullableProperty<string>("Process type");
    }
}