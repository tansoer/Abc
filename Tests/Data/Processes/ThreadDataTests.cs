using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class ThreadDataTests :SealedTests<ThreadData, ProcessElementData> {
        [TestMethod] public void ThreadTypeIdTest() => isNullable<string>();
        [TestMethod] public void ProcessIdTest() => isNullable<string>();
        [TestMethod] public void TerminatorSignatureIdTest() => isNullable<string>();
    }
}
