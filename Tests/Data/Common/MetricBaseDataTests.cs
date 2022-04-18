using Abc.Aids.Random;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Data.Common {

    [TestClass]
    public class MetricBaseDataTests : AbstractTests<MetricBaseData, EntityBaseData> {
        private class testClass : MetricBaseData { }
        protected override MetricBaseData createObject()
            => GetRandom.ObjectOf<testClass>();
    }
}
