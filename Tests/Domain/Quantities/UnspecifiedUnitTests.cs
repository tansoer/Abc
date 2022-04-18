using Abc.Aids.Random;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {

    [TestClass]
    public class UnspecifiedUnitTests : SealedTests<UnspecifiedUnit, Unit> {

        [TestMethod]
        public void ToBaseTest() =>
            Assert.AreEqual(Unspecified.Double, obj.ToBase(GetRandom.Double()));

        [TestMethod]
        public void FromBaseTest() =>
            Assert.AreEqual(Unspecified.Double, obj.FromBase(GetRandom.Double()));
    }

}