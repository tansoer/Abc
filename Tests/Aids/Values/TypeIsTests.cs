using System;
using System.Linq.Expressions;
using Abc.Aids.Random;
using Abc.Aids.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Values {

    [TestClass]
    public class TypeIsTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(TypeIs);
        [TestMethod] public void AnyDecimalTest() { }
        [TestMethod] public void AnyDoubleTest() { }
        [TestMethod] public void AnyIntTest() { }
        [TestMethod] public void AnyLongTest() { }
        [TestMethod] public void TimeSpanTest() => test(GetRandom.TimeSpan(), typeof(TimeSpan));
        [TestMethod] public void DateTimeTest() => test(rndDt, typeof(DateTime));
        [TestMethod] public void StringTest() => test(rndStr, typeof(string));
        [TestMethod] public void BoolTest() => test(rndBool, typeof(bool));
        [TestMethod] public void LongTest() => test(GetRandom.Int64(), typeof(long));
        [TestMethod] public void IntTest() => test(GetRandom.Int32(), typeof(int));
        [TestMethod] public void ShortTest() => test(GetRandom.Int16(), typeof(short));
        [TestMethod] public void SByteTest() => test(GetRandom.Int8(), typeof(sbyte));
        [TestMethod] public void ULongTest() => test(GetRandom.UInt64(), typeof(ulong));
        [TestMethod] public void UIntTest() => test(GetRandom.UInt32(), typeof(uint));
        [TestMethod] public void UShortTest() => test(GetRandom.UInt16(), typeof(ushort));
        [TestMethod] public void ByteTest() => test(GetRandom.UInt8(), typeof(byte));
        [TestMethod] public void FloatTest() => test(GetRandom.Float(), typeof(float));
        [TestMethod] public void DoubleTest() => test(GetRandom.Double(), typeof(double));
        [TestMethod] public void DecimalTest() => test(GetRandom.Decimal(), typeof(decimal));
        [TestMethod] public void ExpressionTest() => test(randomExp(), typeof(Expression));

        private class testExpression :Expression { }
        internal static Expression randomExp() => new testExpression();

        [TestMethod] public void NullTest() {
            areEqual(true, TypeIs.Null(null));
            areEqual(false, TypeIs.Null(GetRandom.AnyValue()));
        }
        private static void test(object x, Type t) {
            testType(x, t);
            testAnyInt(x, t);
            testAnyLong(x, t);
            testAnyDouble(x, t);
            testAnyDecimal(x, t);
        }
        private static void testAnyDouble(object x, Type t) => areEqual(isAnyDouble(t), TypeIs.AnyDouble(x));
        private static void testAnyDecimal(object x, Type t) => areEqual(isAnyDecimal(t), TypeIs.AnyDecimal(x));
        private static void testAnyLong(object x, Type t) => areEqual(isAnyLong(t), TypeIs.AnyLong(x));
        private static void testAnyInt(object x, Type t) => areEqual(isAnyInt(t), TypeIs.AnyInt(x));
        private static bool isAnyDouble(Type t) => t == typeof(double) || t == typeof(float) || isAnyDecimal(t);
        private static bool isAnyDecimal(Type t) => t == typeof(decimal) || t == typeof(ulong) || isAnyLong(t);
        private static bool isAnyLong(Type t) => t == typeof(long) || t == typeof(uint) || isAnyInt(t);
        private static bool isAnyInt(Type t) => t == typeof(byte)
                                                || t == typeof(sbyte)
                                                || t == typeof(short)
                                                || t == typeof(ushort)
                                                || t == typeof(int);
        private static void testType(object x, Type t) {
            areEqual(t == typeof(DateTime), TypeIs.DateTime(x));
            areEqual(t == typeof(bool), TypeIs.Bool(x));
            areEqual(t == typeof(string), TypeIs.String(x));
            areEqual(t == typeof(TimeSpan), TypeIs.TimeSpan(x));
            areEqual(t == typeof(decimal), TypeIs.Decimal(x));
            areEqual(t == typeof(double), TypeIs.Double(x));
            areEqual(t == typeof(float), TypeIs.Float(x));
            areEqual(t == typeof(sbyte), TypeIs.SByte(x));
            areEqual(t == typeof(short), TypeIs.Short(x));
            areEqual(t == typeof(int), TypeIs.Int(x));
            areEqual(t == typeof(long), TypeIs.Long(x));
            areEqual(t == typeof(byte), TypeIs.Byte(x));
            areEqual(t == typeof(ushort), TypeIs.UShort(x));
            areEqual(t == typeof(uint), TypeIs.UInt(x));
            areEqual(t == typeof(ulong), TypeIs.ULong(x));
            areEqual(t == typeof(Expression), TypeIs.Expression(x));
        }
    }
}