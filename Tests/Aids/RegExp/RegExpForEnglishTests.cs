using System.Text.RegularExpressions;
using Abc.Aids.RegExp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.RegExp {

    [TestClass]
    public class RegExpForEnglishTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(RegExpForEnglish);

        [TestMethod]
        public void CapitalsTest() {
            var match = RegExpForEnglish.Capitals;
            Assert.IsTrue(Regex.IsMatch("ABC", match));
            Assert.IsFalse(Regex.IsMatch("ABc", match));
            Assert.IsFalse(Regex.IsMatch("AB ", match));
            Assert.IsFalse(Regex.IsMatch("AB1", match));
        }

        [TestMethod]
        public void TextTest() {
            var match = RegExpForEnglish.Text;
            Assert.IsTrue(Regex.IsMatch("ABC", match));
            Assert.IsTrue(Regex.IsMatch("ABc", match));
            Assert.IsTrue(Regex.IsMatch("AB ", match));
            Assert.IsTrue(Regex.IsMatch("AB'", match));
            Assert.IsTrue(Regex.IsMatch("AB\"", match));
            Assert.IsFalse(Regex.IsMatch("AB1", match));
            Assert.IsFalse(Regex.IsMatch("AB?", match));
            Assert.IsFalse(Regex.IsMatch("aBC", match));
        }

        [TestMethod]
        public void CapitalsAndNumbersTest() {
            var match = RegExpForEnglish.CapitalsAndNumbers;
            Assert.IsTrue(Regex.IsMatch("ABC", match));
            Assert.IsFalse(Regex.IsMatch("ABc", match));
            Assert.IsFalse(Regex.IsMatch("AB ", match));
            Assert.IsFalse(Regex.IsMatch("AB'", match));
            Assert.IsFalse(Regex.IsMatch("AB\"", match));
            Assert.IsTrue(Regex.IsMatch("AB1", match));
            Assert.IsTrue(Regex.IsMatch("A12345", match));
            Assert.IsFalse(Regex.IsMatch("1AB", match));
            Assert.IsFalse(Regex.IsMatch("aBC", match));
        }

    }

}
