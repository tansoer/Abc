using System;
using Abc.Aids.Enums;
using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Extensions {

    [TestClass]
    public class VariableTests : TestsBase {

        [TestInitialize] public virtual void TestInitialize() => type = typeof(Variable);

        [DataRow(typeof(bool))]
        [DataRow(typeof(bool?))]
        [DataRow(typeof(byte))]
        [DataRow(typeof(byte?))]
        [DataRow(typeof(sbyte))]
        [DataRow(typeof(sbyte?))]
        [DataRow(typeof(short))]
        [DataRow(typeof(ushort?))]
        [DataRow(typeof(int))]
        [DataRow(typeof(uint?))]
        [DataRow(typeof(long))]
        [DataRow(typeof(ulong?))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(decimal?))]
        [DataRow(typeof(float))]
        [DataRow(typeof(float?))]
        [DataRow(typeof(double))]
        [DataRow(typeof(double?))]
        [DataRow(typeof(DateTime))]
        [DataRow(typeof(DateTime?))]
        [DataRow(typeof(MonthName))]
        [DataRow(typeof(MonthName?))]
        [DataRow(typeof(IsoGender))]
        [DataRow(typeof(IsoGender?))]
        [DataTestMethod]
        public void ToStringTest(Type t) {
            var v = GetRandom.ValueOf(t);
            var expected = v.ToString();
            var actual = Variable.ToString(v);
            areEqual(expected, actual);
        }

        [TestMethod]
        public void ToStringOldTest() {
            static void test<T>(T x) {
                var expected = x.ToString();
                var actual = Variable.ToString(x);
                Assert.AreEqual(expected, actual);
            }

            test(GetRandom.Int32());
            test(GetRandom.Decimal());
            test(GetRandom.UInt32());
            test(rndBool);
            test(GetRandom.Double(-1000000000, 100000000000));
            test(GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10)).Date);
            Assert.AreEqual(string.Empty, Variable.ToString((int?) null));
            Assert.AreEqual(string.Empty, Variable.ToString((decimal?) null));
            Assert.AreEqual(string.Empty, Variable.ToString((double?) null));
            Assert.AreEqual(string.Empty, Variable.ToString((DateTime?) null));

        }

        [DataRow(typeof(bool))]
        [DataRow(typeof(bool?))]
        [DataRow(typeof(byte))]
        [DataRow(typeof(byte?))]
        [DataRow(typeof(sbyte))]
        [DataRow(typeof(sbyte?))]
        [DataRow(typeof(short))]
        [DataRow(typeof(ushort?))]
        [DataRow(typeof(int))]
        [DataRow(typeof(uint?))]
        [DataRow(typeof(long))]
        [DataRow(typeof(ulong?))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(decimal?))]
        [DataRow(typeof(float))]
        [DataRow(typeof(float?))]
        [DataRow(typeof(double))]
        [DataRow(typeof(double?))]
        [DataRow(typeof(DateTime))]
        [DataRow(typeof(DateTime?))]
        [DataRow(typeof(MonthName))]
        [DataRow(typeof(MonthName?))]
        [DataRow(typeof(IsoGender))]
        [DataRow(typeof(IsoGender?))]
        [DataTestMethod]
        public void TryParseTest(Type t) {
            var expected = GetRandom.ValueOf(t);
            var s = expected.ToString();
            var actual = Variable.TryParse(s, t);
            areEqual(expected.ToString(), actual.ToString());
        }
        [TestMethod]
        public void TryParseOldTest() {
            static void test<T>(T x) {
                var s = x.ToString();
                var actual = Variable.TryParse<T>(s);
                Assert.AreEqual(x, actual);
            }

            test(GetRandom.Int32());
            test(GetRandom.Decimal());
            test(GetRandom.UInt32());
            test(rndBool);
            test(GetRandom.Double(-1000000000, 100000000000));
            test(GetRandom.DateTime(DateTime.Now.AddYears(-10), DateTime.Now.AddYears(10)).Date);
            Assert.AreEqual(0, Variable.TryParse<int>(null));
            Assert.AreEqual(0D, Variable.TryParse<double>(null));
            Assert.AreEqual(0M, Variable.TryParse<decimal>(null));
            Assert.AreEqual(DateTime.MinValue, Variable.TryParse<DateTime>(null));
        }

        [TestMethod]
        public void TryParseDateTimeTest() {
            var dStr = new DateTime(2020, 05, 14, 15, 36, 12).ToString();
            var d = Variable.TryParse<DateTime>(dStr);
            Assert.AreEqual(dStr, Variable.ToString(d));
        }
        [TestMethod]
        public void ToStringDateTimeTest() {
            var dStr = new DateTime(2020, 05, 14, 15, 36, 12).ToString();
            var d = new DateTime(2020, 05, 14, 15, 36, 12);
            Assert.AreEqual(dStr, Variable.ToString(d));
        }


        [DataRow(typeof(bool))]
        [DataRow(typeof(bool?))]
        [DataRow(typeof(byte))]
        [DataRow(typeof(byte?))]
        [DataRow(typeof(sbyte))]
        [DataRow(typeof(sbyte?))]
        [DataRow(typeof(short))]
        [DataRow(typeof(ushort?))]
        [DataRow(typeof(int))]
        [DataRow(typeof(uint?))]
        [DataRow(typeof(long))]
        [DataRow(typeof(ulong?))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(decimal?))]
        [DataRow(typeof(float))]
        [DataRow(typeof(float?))]
        [DataRow(typeof(double))]
        [DataRow(typeof(double?))]
        [DataRow(typeof(DateTime))]
        [DataRow(typeof(DateTime?))]
        [DataRow(typeof(MonthName))]
        [DataRow(typeof(MonthName?))]
        [DataRow(typeof(IsoGender))]
        [DataRow(typeof(IsoGender?))]
        [DataTestMethod]
        public void ParseTest(Type t) {
            var expected = GetRandom.ValueOf(t).ToString();
            var actual = Variable.Parse(expected, t).ToString();
            areEqual(expected, actual);
        }
    }
}
