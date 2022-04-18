using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Extensions {

    [TestClass]
    public class DecimalsTests : TestsBase {

        private decimal d1;
        private decimal absD1;
        private decimal d2;
        private const decimal min = decimal.MinValue;
        private const decimal max = decimal.MaxValue;
        private const decimal zero = decimal.Zero;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(Decimals);
            d1 = GetRandom.Decimal() / 2M;
            d2 = GetRandom.Decimal() / 2M;
            absD1 = System.Math.Abs(d1);
        }

        [TestMethod]
        public void AddTest() {
            Assert.AreEqual(d1 + d2, d1.Add(d2));
            Assert.AreEqual(0, (-d1).Add(d1));
            Assert.AreEqual(0, min.Add(max));
            Assert.AreEqual(max, max.Add(max));
            Assert.AreEqual(max, min.Add(min));
            Assert.AreEqual(max, max.Add(absD1));
            Assert.AreEqual(max - absD1, max.Add(-absD1));
            Assert.AreEqual(min + absD1, min.Add(absD1));
            Assert.AreEqual(max, min.Add(-absD1));
        }

        [TestMethod]
        public void DivideTest() {
            Assert.AreEqual(d1 / d2, d1.Divide(d2));
            Assert.AreEqual(0, zero.Divide(d1));
            Assert.AreEqual(max, d1.Divide(zero));
        }

        [TestMethod]
        public void OppositeTest() {
            Assert.AreEqual(-d1, d1.Opposite());
            Assert.AreEqual(min, max.Opposite());
            Assert.AreEqual(max, min.Opposite());
        }

        [TestMethod]
        public void MultiplyTest() {
            Assert.AreEqual(zero, zero.Multiply(d1));
            Assert.AreEqual(zero, d1.Multiply(zero));
            Assert.AreEqual(max, d1.Multiply(max));
            Assert.AreEqual(max, d1.Multiply(min));
            Assert.AreEqual(d1 * 0.12345M, d1.Multiply(0.12345M));
        }

        [TestMethod]
        public void InverseTest() {
            Assert.AreEqual(1 / d1, d1.Inverse());
            Assert.AreEqual(0, max.Inverse());
            Assert.AreEqual(0, min.Inverse());
            Assert.AreEqual(max, zero.Inverse());
        }

        [TestMethod]
        public void SubtractTest() {
            Assert.AreEqual(d1 - d2, d1.Subtract(d2));
            Assert.AreEqual(0, d1.Subtract(d1));
            Assert.AreEqual(0, max.Subtract(max));
            Assert.AreEqual(0, min.Subtract(min));
            Assert.AreEqual(max, min.Subtract(max));
            Assert.AreEqual(max, max.Subtract(min));
        }

        [TestMethod]
        public void SquareTest() {
            static void test(decimal x) {
                Assert.AreEqual(x.Multiply(x), x.Square());
            }

            test(d1);
            test(d2);
            test(min);
            test(max);
        }

    }

}
