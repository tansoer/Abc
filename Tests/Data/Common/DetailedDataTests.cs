
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Common {
    [TestClass]
    public class DetailedDataTests :AbstractTests<DetailedData, BaseData> {
        private class testClass :DetailedData { }
        protected override DetailedData createObject() => random<testClass>();
        [TestMethod] public void DetailsTest() => isNullable<string>();
    }
}
