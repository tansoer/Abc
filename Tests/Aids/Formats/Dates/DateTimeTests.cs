using System.Globalization;
using Abc.Aids.Formats.Dates;
using Abc.Aids.Random;
using Abc.Aids.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Formats.Dates {
    [TestClass] public class DateTimeTests :TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(DateTime);
        [TestMethod] public void AllTest() {
            foreach (var f in DateTime.All) {
                var expected = rndDt;
                var s = expected.ToString(f);
                var b = System.DateTime.TryParseExact(s, f,
                    UseCulture.Current, DateTimeStyles.None, out var actual);
                Assert.AreEqual(true, b);
                Assert.AreEqual(expected.ToString(f), actual.ToString(f));
            }
        }
        [TestMethod] public void DotTest()
            => Assert.AreEqual("{0}.{1}", DateTime.Dot);
        [TestMethod] public void LongTest()
            => Assert.AreEqual("yyyy-MM-dd HH:mm:ss", DateTime.Long);
        [TestMethod] public void ShortTest()
            => Assert.AreEqual("yyyy-MM-dd HH:mm", DateTime.Short);
        [TestMethod] public void InvariantTest()
            => Assert.AreEqual("{0}T{1}", DateTime.Invariant);
        [TestMethod] public void SpaceTest()
            => Assert.AreEqual("{0} {1}", DateTime.Space);
        [TestMethod] public void IsDateTimeTest() {
            foreach (var f in DateTime.All) {
                if (Date.IsDate(f))
                    Assert.IsFalse(DateTime.IsDateTime(f));
                else if (Time.IsTime(f))
                    Assert.IsFalse(DateTime.IsDateTime(f));
                else Assert.IsTrue(DateTime.IsDateTime(f));
            }
            foreach (var f in Date.All) Assert.IsFalse(DateTime.IsDateTime(f));
            foreach (var f in Time.All) Assert.IsFalse(DateTime.IsDateTime(f));
            Assert.IsFalse(DateTime.IsDateTime(null));
            Assert.IsFalse(DateTime.IsDateTime(""));
            Assert.IsFalse(DateTime.IsDateTime("  "));
            Assert.IsFalse(DateTime.IsDateTime(rndStr));
        }
        [TestMethod] public void IsCorrectTest() {
            foreach (var f in DateTime.All) Assert.IsTrue(DateTime.IsCorrect(f));
            foreach (var f in Date.All) Assert.IsTrue(DateTime.IsCorrect(f));
            foreach (var f in Time.All) Assert.IsTrue(DateTime.IsCorrect(f));
            Assert.IsFalse(DateTime.IsCorrect(null));
            Assert.IsFalse(DateTime.IsCorrect(""));
            Assert.IsFalse(DateTime.IsCorrect("  "));
            Assert.IsFalse(DateTime.IsCorrect(rndStr));
        }
    }
}
