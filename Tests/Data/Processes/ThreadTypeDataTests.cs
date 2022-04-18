using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class ThreadTypeDataTests :SealedTests<ThreadTypeData, ProcessElementTypeData> {
        [TestMethod] public void ProcessTypeIdTest() => isNullable<string>();
    }
}
