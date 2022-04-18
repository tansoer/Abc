using Abc.Facade.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Facade.Processes {
    [TestClass] public class ThreadViewTests :SealedTests<ThreadView, ProcessElementView> {
        [TestMethod] public void ThreadTypeIdTest() => isNullableProperty<string>("Thread type");
        [TestMethod] public void ProcessIdTest() => isNullableProperty<string>("Process");
        [TestMethod] public void TerminatorSignatureIdTest() => isNullableProperty<string>("Terminator signature");
    }
}