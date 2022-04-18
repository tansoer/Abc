using System;
using System.Linq.Expressions;
using Abc.Aids.Random;
using Abc.Aids.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Values {

    [TestClass]
    public class TypesAreTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(TypesAre);

        [TestMethod] public void AnyDecimalTest() { }

        [TestMethod] public void AnyDoubleTest() { }

        [TestMethod] public void AnyIntTest() { }

        [TestMethod] public void AnyLongTest() { }

        [TestMethod] public void TimeSpanTest() => test(GetRandom.TimeSpan);

        [TestMethod] public void DateTimeTest() => test(() => rndDt);

        [TestMethod] public void StringTest() => test(() => rndStr);

        [TestMethod] public void BoolTest() => test(GetRandom.Bool);

        [TestMethod] public void LongTest() => test(() => GetRandom.Int64());

        [TestMethod] public void IntTest() => test(() => GetRandom.Int32());

        [TestMethod] public void ShortTest() => test(() => GetRandom.Int16());

        [TestMethod] public void SByteTest() => test(() => GetRandom.Int8());

        [TestMethod] public void ULongTest() => test(() => GetRandom.UInt64());

        [TestMethod] public void UIntTest() => test(() => GetRandom.UInt32());

        [TestMethod] public void UShortTest() => test(() => GetRandom.UInt16());

        [TestMethod] public void ByteTest() => test(() => GetRandom.UInt8());

        [TestMethod] public void FloatTest() => test(() => GetRandom.Float());

        [TestMethod] public void DoubleTest() => test(() => GetRandom.Double());

        [TestMethod] public void DecimalTest() => test(() => GetRandom.Decimal());
        [TestMethod] public void ExpressionTest() => test(() => TypeIsTests.randomExp());

        [TestMethod]
        public void NullTest() {
            var x = GetRandom.List(GetRandom.AnyValue).ToArray();
            Assert.AreEqual(false, TypesAre.Null(x));
            x = GetRandom.List<object>(() => null).ToArray();
            Assert.AreEqual(true, TypesAre.Null(x));
        }

        private static void test<T>(Func<T> f) {
            var x = GetRandom.List(() => f() as object).ToArray();
            testType(x, typeof(T));
            testAnyInt(x, typeof(T));
            testAnyLong(x, typeof(T));
            testAnyDouble(x, typeof(T));
            testAnyDecimal(x, typeof(T));
        }

        private static void testAnyDouble(object[] x, Type t) {
            var expected = isAnyDouble(t);
            Assert.AreEqual(expected, TypesAre.AnyDouble(x));
        }

        private static void testAnyDecimal(object[] x, Type t) {
            var expected = isAnyDecimal(t);
            Assert.AreEqual(expected, TypesAre.AnyDecimal(x));
        }

        private static void testAnyLong(object[] x, Type t) {
            var expected = isAnyLong(t);
            Assert.AreEqual(expected, TypesAre.AnyLong(x));
        }

        private static void testAnyInt(object[] x, Type t) {
            var expected = isAnyInt(t);
            Assert.AreEqual(expected, TypesAre.AnyInt(x));
        }

        private static bool isAnyDouble(Type t) => t == typeof(double)
                                                   || t == typeof(float)
                                                   || isAnyDecimal(t);

        private static bool isAnyDecimal(Type t) => t == typeof(decimal)
                                                    || t == typeof(ulong)
                                                    || isAnyLong(t);

        private static bool isAnyLong(Type t) => t == typeof(long)
                                                 || t == typeof(uint)
                                                 || isAnyInt(t);

        private static bool isAnyInt(Type t) => t == typeof(byte)
                                                || t == typeof(sbyte)
                                                || t == typeof(short)
                                                || t == typeof(ushort)
                                                || t == typeof(int);

        private static void testType(object[] x, Type t) {
            Assert.AreEqual(t == typeof(DateTime), TypesAre.DateTime(x));
            Assert.AreEqual(t == typeof(bool), TypesAre.Bool(x));
            Assert.AreEqual(t == typeof(string), TypesAre.String(x));
            Assert.AreEqual(t == typeof(TimeSpan), TypesAre.TimeSpan(x));
            Assert.AreEqual(t == typeof(decimal), TypesAre.Decimal(x));
            Assert.AreEqual(t == typeof(double), TypesAre.Double(x));
            Assert.AreEqual(t == typeof(float), TypesAre.Float(x));
            Assert.AreEqual(t == typeof(sbyte), TypesAre.SByte(x));
            Assert.AreEqual(t == typeof(short), TypesAre.Short(x));
            Assert.AreEqual(t == typeof(int), TypesAre.Int(x));
            Assert.AreEqual(t == typeof(long), TypesAre.Long(x));
            Assert.AreEqual(t == typeof(byte), TypesAre.Byte(x));
            Assert.AreEqual(t == typeof(ushort), TypesAre.UShort(x));
            Assert.AreEqual(t == typeof(uint), TypesAre.UInt(x));
            Assert.AreEqual(t == typeof(ulong), TypesAre.ULong(x));
            Assert.AreEqual(t == typeof(Expression), TypesAre.Expression(x));
        }

    }

}