using Abc.Data.Common;
using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class ProcessTypeDataTests :SealedTests<ProcessTypeData, EntityTypeData> {
        [TestMethod] public void RuleSetIdTest() => isNullable<string>();
    }
}
