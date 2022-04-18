using System;
using Abc.Aids.Random;
using Abc.Aids.Regions;
using Abc.Aids.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Values {
    [TestClass] public class ConvertToTests :TestsBase {
        private decimal m;
        private TimeSpan ts;
        [TestInitialize] public void TestInitialize() {
            type = typeof(ConvertTo);
            m = GetRandom.Decimal() / 2M;
            ts = GetRandom.TimeSpan();
        }
        [TestMethod] public void YearsTest() => areEqual(ConvertTo.Years(ts), ts.Years());
        [TestMethod] public void MonthsTest() => areEqual(ConvertTo.Months(ts), ts.Months());
        [TestMethod] public void DaysTest() => areEqual(ConvertTo.Days(ts), ts.Days());
        [TestMethod] public void HoursTest() => areEqual(ConvertTo.Hours(ts), ts.Hours());
        [TestMethod] public void MinutesTest() => areEqual(ConvertTo.Minutes(ts), ts.Minutes());
        [TestMethod] public void SecondsTest() => areEqual(ConvertTo.Seconds(ts), ts.Seconds());
        [TestMethod] public void DecimalTest() {
            var d = GetRandom.Double(Convert.ToSingle(decimal.MinValue), Convert.ToSingle(decimal.MaxValue));
            var f = GetRandom.Float(Convert.ToSingle(decimal.MinValue), Convert.ToSingle(decimal.MaxValue));
            var l = GetRandom.Int64();
            var i = GetRandom.Int32();
            var s = GetRandom.Int16();
            var b = GetRandom.Int8();
            var ul = GetRandom.UInt64();
            var ui = GetRandom.UInt32();
            var us = GetRandom.UInt16();
            var ub = GetRandom.UInt8();
            areEqual(m, ConvertTo.Decimal(m));
            areEqual(Convert.ToDecimal(d), ConvertTo.Decimal(d));
            areEqual(Convert.ToDecimal(f), ConvertTo.Decimal(f));
            areEqual(Convert.ToDecimal(l), ConvertTo.Decimal(l));
            areEqual(Convert.ToDecimal(i), ConvertTo.Decimal(i));
            areEqual(Convert.ToDecimal(s), ConvertTo.Decimal(s));
            areEqual(Convert.ToDecimal(b), ConvertTo.Decimal(b));
            areEqual(Convert.ToDecimal(ul), ConvertTo.Decimal(ul));
            areEqual(Convert.ToDecimal(ui), ConvertTo.Decimal(ui));
            areEqual(Convert.ToDecimal(us), ConvertTo.Decimal(us));
            areEqual(Convert.ToDecimal(ub), ConvertTo.Decimal(ub));
            areEqual(1.2345M, ConvertTo.Decimal("1.2345"));
            areEqual(1.2345M, ConvertTo.Decimal(1.2345D));
            areEqual(1.2345M, ConvertTo.Decimal(1.2345F));
        }
        [TestMethod] public void StringTest() {
            static void action(decimal x) => areEqual(x.ToString(UseCulture.Invariant), ConvertTo.String(x));
            action(GetRandom.Decimal());
            action(1234.567m);
            action(1234567m);
            action(123456.7m);
            Assert.AreEqual(m.ToString(UseCulture.English), ConvertTo.String(m));
        }
        [TestMethod] public void TryParseTest() {
            static void action(decimal x, string s) {
                Assert.IsTrue(ConvertTo.Decimal(s, out var y));
                Assert.AreEqual(x, y);
            }
            action(m, m.ToString(UseCulture.Invariant));
            action(1234.567m, "1234.567");
            action(1234567m, "1234,567");
            action(123456.7m, "1234,56.7");
        }
    }
}