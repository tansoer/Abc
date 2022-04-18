using Abc.Aids.Random;
using Abc.Data.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Data.Common {
    [TestClass]
    public class BaseDataTests :AbstractTests<BaseData, object> {
        private class testClass :BaseData { }
        protected override BaseData createObject() => random<testClass>();
        [TestMethod] public void IdTest() => isNullable<string>();
        [TestMethod] public void ValidFromTest() => isNullable<DateTime?>();
        [TestMethod] public void ValidToTest() => isNullable<DateTime?>();
        [TestMethod] public void RecordedTest() => isNullable<DateTime?>();
        [TestMethod] public void ReplacedTest() => isNullable<DateTime?>();
        [TestMethod] public void RecordedByTest() => isNullable<string>();
        [TestMethod] public void RecordedWhyTest() => isNullable<string>();
        [TestMethod] public void TimestampTest() => isNullable<byte[]>();
        [TestMethod] public void ToStringTest()
            => areEqual($"Id = {obj.Id} ({obj.GetType().FullName})", obj.ToString());
    }
}