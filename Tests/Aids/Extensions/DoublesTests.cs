using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Abc.Aids.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Extensions {

    [TestClass]
    public class DoublesTests : TestsBase {

        private double x;
        private double y;
        private const double nan = double.NaN;
        private const double max = double.MaxValue;
        private const double min = double.MinValue;
        private const double pInf = double.PositiveInfinity;
        private const double nInf = double.NegativeInfinity;

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(Doubles);
            x = GetRandom.Double(-1000000, 1000000);
            y = GetRandom.Double(-1000000, 1000000);
        }

        [TestMethod]
        public void AddTest() {
            static void add(double a, double b, double sum) => Assert.AreEqual(sum, a.Add(b));
            add(2, 3, 5);
            add(x, y, x + y);
            add(max, max, pInf);
            add(min, min, nInf);
            add(pInf, pInf, pInf);
            add(nInf, nInf, nInf);
            add(nan, y, nan);
            add(nan, max, nan);
            add(nan, min, nan);
            add(nan, pInf, nan);
            add(nan, nInf, nan);
            add(y, nan, nan);
            add(max, nan, nan);
            add(min, nan, nan);
            add(pInf, nan, nan);
            add(nInf, nan, nan);
        }

        [TestMethod]
        public void DivideTest() {
            static void div(double a, double b, double sum) => Assert.AreEqual(sum, a.Divide(b));
            div(10, 5, 2);
            div(x, y, x / y);
            div(max, max, 1D);
            div(min, min, 1D);
            div(pInf, pInf, nan);
            div(nInf, nInf, nan);
            div(nan, y, nan);
            div(nan, max, nan);
            div(nan, min, nan);
            div(nan, pInf, nan);
            div(nan, nInf, nan);
            div(x, nan, nan);
            div(max, nan, nan);
            div(min, nan, nan);
            div(pInf, nan, nan);
            div(nInf, nan, nan);
            div(System.Math.Abs(x), 0D, pInf);
            div(-System.Math.Abs(x), 0D, nInf);
            div(max, 0D, pInf);
            div(min, 0D, nInf);
            div(pInf, 0D, pInf);
            div(nInf, 0D, nInf);
        }

        [TestMethod]
        public void OppositeTest() {
            Assert.AreEqual(-3, 3d.Opposite());
            Assert.AreEqual(-x, x.Opposite());
            Assert.AreEqual(x, (-x).Opposite());
            Assert.AreEqual(0D, 0D.Opposite());
            Assert.AreEqual(pInf, nInf.Opposite());
            Assert.AreEqual(nInf, pInf.Opposite());
            Assert.AreEqual(min, max.Opposite());
            Assert.AreEqual(max, min.Opposite());
            Assert.AreEqual(nan, nan.Opposite());
            Assert.AreEqual(-double.Epsilon, double.Epsilon.Opposite());
            Assert.AreEqual(double.Epsilon, (-double.Epsilon).Opposite());
        }

        [TestMethod]
        public void MultiplyTest() {
            static void test(double a, double b, double product)
                => Assert.AreEqual(product, a.Multiply(b));

            test(3, 5, 15);
            test(x, y, x * y);
            test(max, max, pInf);
            test(min, min, pInf);
            test(pInf, pInf, pInf);
            test(nInf, nInf, pInf);
            test(nan, y, nan);
            test(nan, max, nan);
            test(nan, min, nan);
            test(nan, pInf, nan);
            test(nan, nInf, nan);
            test(x, nan, nan);
            test(max, nan, nan);
            test(min, nan, nan);
            test(pInf, nan, nan);
            test(nInf, nan, nan);
            test(System.Math.Abs(x), 0D, 0D);
            test(-System.Math.Abs(x), 0D, 0D);
            test(max, 0D, 0D);
            test(min, 0D, 0D);
            test(pInf, 0D, nan);
            test(nInf, 0D, nan);
        }

        [TestMethod]
        public void PowerTest() {
            static void test(double a, double b, double power)
                => Assert.AreEqual(power, a.Power(b));

            test(2, 4, 16);
            test(x, y, System.Math.Pow(x, y));
            test(max, max, pInf);
            test(min, min, 0D);
            test(pInf, pInf, pInf);
            test(nInf, nInf, 0D);
            test(nan, y, nan);
            test(nan, max, nan);
            test(nan, min, nan);
            test(nan, pInf, nan);
            test(nan, nInf, nan);
            test(y, nan, nan);
            test(max, nan, nan);
            test(min, nan, nan);
            test(pInf, nan, nan);
            test(nInf, nan, nan);
            test(System.Math.Abs(y), 0D, 1D);
            test(-System.Math.Abs(y), 0D, 1D);
            test(max, 0D, 1D);
            test(min, 0D, 1D);
            test(pInf, 0D, 1D);
            test(nInf, 0D, 1D);
        }

        [TestMethod]
        public void InverseTest() {
            Assert.AreEqual(10, 0.1d.Inverse());
            Assert.AreEqual(1 / x, x.Inverse());
            Assert.AreEqual(x, (1 / x).Inverse(), x.Delta());
            Assert.AreEqual(pInf, 0D.Inverse());
            Assert.AreEqual(0D, nInf.Inverse());
            Assert.AreEqual(0D, pInf.Inverse());
            Assert.AreEqual(1 / max, max.Inverse());
            Assert.AreEqual(1 / min, min.Inverse());
            Assert.AreEqual(nan, nan.Inverse());
            Assert.AreEqual(pInf, double.Epsilon.Inverse());
            Assert.AreEqual(nInf, (-double.Epsilon).Inverse());
        }

        [TestMethod]
        public void SqrtTest() {
            static void test(double a, double sqrt)
                => Assert.AreEqual(sqrt, a.Sqrt());

            test(25, 5);
            test(x, x.Sqrt());
            test(max, max.Sqrt());
            test(min, min.Sqrt());
            test(pInf, pInf);
            test(nInf, nan);
            test(nan, nan);
            test(0D, 0D);
        }

        [TestMethod]
        public void SquareTest() {
            static void test(double a, double square)
                => Assert.AreEqual(square, a.Square());

            test(10D, 100D);
            test(x, x * x);
            test(max, pInf);
            test(min, pInf);
            test(pInf, pInf);
            test(nInf, pInf);
            test(nan, nan);
            test(0D, 0D);
        }

        [TestMethod]
        public void SubtractTest() {
            static void test(double a, double b, double dif)
                => Assert.AreEqual(dif, a.Subtract(b));

            test(10, 7, 3);

            test(x, y, x - y);
            test(max, max, 0D);
            test(min, min, 0D);
            test(pInf, pInf, nan);
            test(nInf, nInf, nan);
            test(nan, y, nan);
            test(nan, max, nan);
            test(nan, min, nan);
            test(nan, pInf, nan);
            test(nan, nInf, nan);
            test(x, nan, nan);
            test(max, nan, nan);
            test(min, nan, nan);
            test(pInf, nan, nan);
            test(nInf, nan, nan);
        }

        [TestMethod]
        public void DeltaTest() {
            const double epsilon = 1E7;
            Assert.AreEqual(1D, 10000000D.Delta());
            Assert.AreEqual(0D, 0D.Delta());
            Assert.AreEqual(1 / epsilon, Doubles.Delta(1));
            Assert.AreEqual(100 / epsilon, Doubles.Delta(100));
            Assert.AreEqual(nan, nan.Delta());
            Assert.AreEqual(pInf, pInf.Delta());
            Assert.AreEqual(pInf, nInf.Delta());
            Assert.AreEqual(0D, double.Epsilon.Delta());
            Assert.AreEqual(max / epsilon, max.Delta());
            Assert.AreEqual(max / epsilon, min.Delta());
        }
        [TestMethod]
        public void ToDoubleTest() {
            Assert.AreEqual(0D, Doubles.ToDouble(rndStr));
            Assert.AreEqual(1.2345D, Doubles.ToDouble("1.2345"));
            Assert.AreEqual(1.2345D, Doubles.ToDouble((float) 1.2345), 1.2345D.Delta());
            Assert.AreEqual(1.2345D, Doubles.ToDouble(1.2345));
            Assert.AreEqual(-12345.0D, Doubles.ToDouble((long) -12345));
            Assert.AreEqual(12345.0D, Doubles.ToDouble((ulong) 12345));
            Assert.AreEqual(-12345.0D, Doubles.ToDouble((short) -12345));
            Assert.AreEqual(12345.0D, Doubles.ToDouble((ushort) 12345));
            Assert.AreEqual(-12345.0D, Doubles.ToDouble(-12345));
            Assert.AreEqual(12345.0D, Doubles.ToDouble((uint) 12345));
            Assert.AreEqual(123.0D, Doubles.ToDouble((byte) 123));
            Assert.AreEqual(-123.0D, Doubles.ToDouble((sbyte) -123));
            Assert.AreEqual(1.2345D, Doubles.ToDouble((decimal) 1.2345));
            Assert.AreEqual((double) decimal.MaxValue, Doubles.ToDouble(decimal.MaxValue));
            Assert.AreEqual((double) decimal.MinValue, Doubles.ToDouble(decimal.MinValue));
        }
        [TestMethod]
        public void TryParseTest() {
            var d = GetRandom.Double();
            Assert.IsFalse(Doubles.TryParse(rndStr, out var z));
            Assert.AreEqual(0, z);
            Assert.IsTrue(Doubles.TryParse(d.ToString(UseCulture.Invariant), out z));
            Assert.AreEqual(d, z, d.Delta());
        }

    }

}