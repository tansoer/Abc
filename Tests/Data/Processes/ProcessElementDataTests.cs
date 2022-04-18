using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class ProcessElementDataTests :AbstractTests<ProcessElementData, ProcessElementBaseData> {
        private class testClass :ProcessElementData { }
        [TestMethod] public void RuleContextIdTest() => isNullable<string>();
        protected override ProcessElementData createObject() => random<testClass>();
    }
}
