using Abc.Facade.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Abc.Tests.Facade.Common {
    [TestClass] public class BaseViewTests :
        AbstractTests<BaseView, object> {
        private class testClass :BaseView { }
        protected override BaseView createObject() => random<testClass>();
        [TestMethod] public void IdTest() => isNullableProperty<string>("Id", true);
        [TestMethod] public void ValidFromTest() => isNullableProperty<DateTime?>("Valid from");
        [TestMethod] public void ValidToTest() => isNullableProperty<DateTime?>("Valid to");
        [TestMethod] public void RecordedTest() => isNullableProperty<DateTime?>("Recorded");
        [TestMethod] public void ReplacedTest() => isNullableProperty<DateTime?>("Replaced");
        [TestMethod] public void RecordedByTest() => isNullableProperty<string>("Recorded by");
        [TestMethod] public void RecordedWhyTest() => isNullableProperty<string>("Recorded why");
        [TestMethod] public void TimestampTest() => isNullable<byte[]>();
    }
}