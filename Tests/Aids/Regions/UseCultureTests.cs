using System.Globalization;
using Abc.Aids.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Regions {

    [TestClass]
    public class UseCultureTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(UseCulture);

        [TestMethod]
        public void CurrentTest() {
            var current = CultureInfo.CurrentCulture;
            Assert.AreEqual(current, UseCulture.Current);
        }

        [TestMethod]
        public void EnglishTest() {
            var current = new CultureInfo("en-GB");
            Assert.AreEqual(current, UseCulture.English);
        }

        [TestMethod]
        public void InvariantTest() {
            var current = new CultureInfo("");
            Assert.AreEqual(current, UseCulture.Invariant);
        }

    }

}