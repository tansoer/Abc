using System;
using Abc.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abc.Aids.Constants;
using Abc.Aids.Random;

namespace Abc.Tests.Domain.Common {
    [TestClass] public class UnspecifiedTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(Unspecified);
        [TestMethod] public void StringTest() => areEqual(Unspecified.String, Word.Unspecified);
        [TestMethod] public void ValidFromDateTest() => areEqual(Unspecified.ValidFromDate, DateTime.MinValue);
        [TestMethod] public void ValidToDateTest() => areEqual(Unspecified.ValidToDate, DateTime.MaxValue);
        [TestMethod] public void DoubleTest() => areEqual(Unspecified.Double, double.NaN);
        [TestMethod] public void DecimalTest() => areEqual(Unspecified.Decimal, decimal.MaxValue);
        [TestMethod] public void IntegerTest() => areEqual(Unspecified.Integer, int.MaxValue);
        [TestMethod] public void UIntTest() => areEqual(Unspecified.UInt, uint.MaxValue);
        [TestMethod] public void ByteTest() => areEqual(Unspecified.Byte, byte.MaxValue);
        [TestMethod] public void IsUnspecifiedTest() {
            testString();
            testByte();
            testUInt();
            testInteger();
            testDouble();
            testDecimal();
            testDateTime();
        }
        private static void testString() {
            isFalse(Unspecified.IsUnspecified("dummy"));
            isTrue(Unspecified.IsUnspecified(""));
            isTrue(Unspecified.IsUnspecified(null));
            isTrue(Unspecified.IsUnspecified(Word.Unspecified));
        }
        private static void testByte() {
            isTrue(Unspecified.IsUnspecified(byte.MaxValue));
            isFalse(Unspecified.IsUnspecified(byte.MinValue));
        }
        private static void testUInt() {
            isTrue(Unspecified.IsUnspecified(uint.MaxValue));
            isFalse(Unspecified.IsUnspecified(uint.MinValue));
        }
        private static void testInteger() {
            isTrue(Unspecified.IsUnspecified(int.MaxValue));
            isFalse(Unspecified.IsUnspecified(int.MinValue));
        }
        private static void testDouble() {
            isFalse(Unspecified.IsUnspecified(GetRandom.Double()));
            isTrue(Unspecified.IsUnspecified(double.NaN));
        }
        private static void testDecimal() {
            isTrue(Unspecified.IsUnspecified(decimal.MaxValue));
            isFalse(Unspecified.IsUnspecified(decimal.Zero));
        }
        private static void testDateTime() {
            isFalse(Unspecified.IsUnspecified(DateTime.Now, false));
            isFalse(Unspecified.IsUnspecified(DateTime.Now, true));
            isTrue(Unspecified.IsUnspecified(DateTime.MinValue, true));
            isTrue(Unspecified.IsUnspecified(DateTime.MaxValue, false));
        }
    }
}