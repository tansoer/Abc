using Abc.Data.Common;
using Abc.Data.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Processes {
    [TestClass]
    public class ProcessElementBaseDataTests :AbstractTests<ProcessElementBaseData, EntityBaseData> {
        private class testClass :ProcessElementBaseData { }
        [TestMethod] public void NextElementIdTest() => isNullable<string>();
        [TestMethod] public void PreviousElementIdTest() => isNullable<string>();
        protected override ProcessElementBaseData createObject() => random<testClass>();
    }
}
