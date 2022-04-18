using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class ProcessElementTypeDataTests :AbstractTests<ProcessElementTypeData, ProcessElementBaseData> {
        private class testClass :ProcessElementTypeData { }
        [TestMethod] public void RuleSetIdTest() => isNullable<string>();
        [TestMethod] public void BaseTypeIdTest() => isNullable<string>();
        protected override ProcessElementTypeData createObject() => random<testClass>();
    }
}
