using System;
using System.Linq.Expressions;
using Abc.Aids.Calculator;
using Abc.Aids.Random;
using Abc.Aids.Values;
using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Values {
    [TestClass]
    public class ExprTests :TestsBase {
        private object o1;
        private object o2;
        private DateTime dt1;
        private DateTime dt2;
        private object do1;
        private object do2;
        private object i1;
        private object i2;
        private string s1;
        private string s2;
        private bool b1;
        private bool b2;
        private decimal de1;
        private decimal de2;
        private TimeSpan ts1;
        private TimeSpan ts2;

        internal class testClass {
            public string String { get; set; }
            public DateTime DateTime { get; set; }
            public bool Bool { get; set; }
            public double Double { get; set; }
            public int Integer { get; set; }
        }

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(Expr);
            o1 = GetRandom.AnyValue();
            o2 = GetRandom.AnyValue();
            while (o1.ToString() == o2.ToString()) o2 = GetRandom.AnyValue();
            dt1 = GetRandom.DateTime(DateTime.Now.AddYears(-100), DateTime.Now).AddYears(100);
            dt2 = GetRandom.DateTime(dt1, dt1.AddYears(100));
            ts1 = GetRandom.TimeSpan();
            ts2 = GetRandom.TimeSpan();
            do1 = GetRandom.Double(-1000000, 1000000);
            do2 = GetRandom.Double(-1000000, 1000000);
            i1 = rndInt;
            i2 = rndInt;
            s1 = rndStr;
            s2 = rndStr;
            b1 = rndBool;
            b2 = rndBool;
            de1 = GetRandom.Decimal(-1000000, 1000000);
            de2 = GetRandom.Decimal(-1000000, 1000000);
        }

        [DataRow(Operation.GetSecond)]
        [DataRow(Operation.GetMinute)]
        [DataRow(Operation.GetHour)]
        [DataRow(Operation.GetDay)]
        [DataRow(Operation.GetMonth)]
        [DataRow(Operation.GetYear)]
        [DataRow(Operation.GetAge)]
        [DataRow(Operation.ToSeconds)]
        [DataRow(Operation.ToMinutes)]
        [DataRow(Operation.ToHours)]
        [DataRow(Operation.ToDays)]
        [DataRow(Operation.ToMonths)]
        [DataRow(Operation.ToYears)]
        [TestMethod]
        public void ComposeDateTimeExprTest(Operation o) {
            var d = GetRandom.DateTime(DateTime.Now.AddYears(-50), DateTime.Now);
            var param = Expression.Parameter(typeof(testClass), "x");
            var body = Expr.Compose(param, o, nameof(testClass.DateTime));
            isNotNull(body);
            var obj = new testClass() { DateTime = d };
            var expected = o switch {
                Operation.GetSecond => d.Second,
                Operation.GetMinute => d.Minute,
                Operation.GetHour => d.Hour,
                Operation.GetDay => d.Day,
                Operation.GetMonth => d.Month,
                Operation.GetYear => d.Year,
                Operation.GetAge => Compute.GetAge(d),
                Operation.ToSeconds => Compute.ToSeconds(d),
                Operation.ToMinutes => Compute.ToMinutes(d),
                Operation.ToHours => Compute.ToHours(d),
                Operation.ToDays => Compute.ToDays(d),
                Operation.ToMonths => Compute.ToMonths(d),
                Operation.ToYears => Compute.ToYears(d),
                _ => throw new NotImplementedException(),
            };
            var expression = Expression.Lambda<Func<testClass, object>>(body, param);
            var actual = expression.Compile().Invoke(obj);
            areEqual(actual, expected);
        }

        [DataRow(Operation.Inverse, nameof(testClass.Double), 0.1, 10.0)]
        [DataRow(Operation.Opposite, nameof(testClass.Double), -1.0, 1.0)]
        [DataRow(Operation.Square, nameof(testClass.Double), 2.0, 4.0)]
        [DataRow(Operation.Sqrt, nameof(testClass.Double), 4.0, 2.0)]
        [DataRow(Operation.Not, nameof(testClass.Bool), true, false)]
        [DataRow(Operation.GetLength, nameof(testClass.String), "abc", 3)]
        [DataRow(Operation.ToUpper, nameof(testClass.String), "abc", "ABC")]
        [DataRow(Operation.ToLower, nameof(testClass.String), "ABC", "abc")]
        [DataRow(Operation.Trim, nameof(testClass.String), "   abc   ", "abc")]
        [TestMethod]
        public void ComposeUnaryExprTest(Operation o, string property, object x, object expected) {
            var param = Expression.Parameter(typeof(testClass), "x");
            var body = Expr.Compose(param, o, property);
            isNotNull(body);
            var obj = new testClass();
            if (property == nameof(testClass.String)) obj.String = (string)x;
            else if (property == nameof(testClass.Bool)) obj.Bool = (bool)x;
            else if (property == nameof(testClass.Double)) obj.Double = (double)x;
            var expression = Expression.Lambda<Func<testClass, object>>(body, param);
            var actual = expression.Compile().Invoke(obj);
            areEqual(expected, actual);
        }

        [DataRow(Operation.IsEqual, nameof(testClass.String), "AAA", "AAA", true)]
        [DataRow(Operation.IsEqual, nameof(testClass.String), "AAA", "B", false)]
        [DataRow(Operation.Add, nameof(testClass.String), "AAA", "B", "AAAB")]
        [DataRow(Operation.Add, nameof(testClass.Double), 2.0, 3.0, 5.0)]
        [DataRow(Operation.Subtract, nameof(testClass.Double), 5.0, 3.0, 2.0)]
        [DataRow(Operation.Multiply, nameof(testClass.Double), 2.0, 3.0, 6.0)]
        [DataRow(Operation.Divide, nameof(testClass.Double), 3.0, 2.0, 1.5)]
        [DataRow(Operation.Power, nameof(testClass.Double), 2.0, 4.0, 16.0)]
        [DataRow(Operation.And, nameof(testClass.Bool), true, true, true)]
        [DataRow(Operation.Or, nameof(testClass.Bool), true, false, true)]
        [DataRow(Operation.Xor, nameof(testClass.Bool), true, true, false)]
        [DataRow(Operation.IsEqual, nameof(testClass.Bool), true, true, true)]
        [DataRow(Operation.IsEqual, nameof(testClass.Double), 1.4, 1.4, true)]
        [DataRow(Operation.IsGreater, nameof(testClass.Double), 1.4, 1.2, true)]
        [DataRow(Operation.IsLess, nameof(testClass.Double), 1.4, 1.2, false)]
        [DataRow(Operation.Substring, nameof(testClass.String), "1234567890", 5, "67890")]
        [DataRow(Operation.Contains, nameof(testClass.String), "1234567890", "a", false)]
        [DataRow(Operation.Contains, nameof(testClass.String), "1234567890", "3456", true)]
        [DataRow(Operation.EndsWith, nameof(testClass.String), "1234567890", "890", true)]
        [DataRow(Operation.EndsWith, nameof(testClass.String), "1234567890", "123", false)]
        [DataRow(Operation.StartsWith, nameof(testClass.String), "1234567890", "123", true)]
        [DataRow(Operation.StartsWith, nameof(testClass.String), "1234567890", "345", false)]
        [TestMethod]
        public void ComposeBinaryExprTest(Operation o, string property, object x, object y, object expected) {
            var s = random<string>();
            var param = Expression.Parameter(typeof(testClass), "x");
            var a = ValueFactory.Create(s, y);
            var body = Expr.Compose(param, o, property, a);
            isNotNull(body);
            var obj = new testClass();
            if (property == nameof(testClass.String)) obj.String = (string)x;
            else if (property == nameof(testClass.Bool)) obj.Bool = (bool)x;
            else if (property == nameof(testClass.Double)) obj.Double = (double)x;
            var expression = Expression.Lambda<Func<testClass, object>>(body, param);
            var actual = expression.Compile().Invoke(obj);
            areEqual(expected, actual);
        }

        [DataRow(Operation.GetInterval)]
        [DataRow(Operation.AddSeconds)]
        [DataRow(Operation.AddMinutes)]
        [DataRow(Operation.AddHours)]
        [DataRow(Operation.AddDays)]
        [DataRow(Operation.AddMonths)]
        [DataRow(Operation.AddYears)]
        [TestMethod] public void ComposeBinaryDateTimeExprTest(Operation o) {
            var d = GetRandom.DateTime(DateTime.Now.AddYears(-50), DateTime.Now);
            var param = Expression.Parameter(typeof(testClass), "x");
            var years = GetRandom.Int32(10, 50);
            dynamic x = o switch {
                Operation.GetInterval => d.AddYears(years),
                Operation.AddSeconds => 60 * 60 * 24 * 365.25 * years,
                Operation.AddMinutes => 60 * 24 * 365.25 * years,
                Operation.AddHours => 24 * 365.25 * years,
                Operation.AddDays => 365.25 * years,
                Operation.AddMonths => 12 * years,
                Operation.AddYears => years,
                _ => throw new NotImplementedException(),
            };
            var arg = ValueFactory.Create(random<string>(), x);
            var body = Expr.Compose(param, o, nameof(testClass.DateTime), arg);
            isNotNull(body);
            var obj = new testClass() { DateTime = d };
            dynamic expected = o switch {
                Operation.GetInterval => Compute.GetInterval(d, x),
                Operation.AddSeconds => Compute.AddSeconds(d, x),
                Operation.AddMinutes => Compute.AddMinutes(d, x),
                Operation.AddHours => Compute.AddHours(d, x),
                Operation.AddDays => Compute.AddDays(d, x),
                Operation.AddMonths => Compute.AddMonths(d, x),
                Operation.AddYears => Compute.AddYears(d, x),
                _ => throw new NotImplementedException(),
            };
            var expression = Expression.Lambda<Func<testClass, object>>(body, param);
            var actual = expression.Compile().Invoke(obj);
            if (o == Operation.GetInterval) mostlyEqual( actual, expected);
            else areEqual(actual, expected);
        }

        [DataRow(Operation.Substring, nameof(testClass.String), "123456789", 3, 4, "4567")]
        [DataRow(Operation.Substring, nameof(testClass.String), "123456789", 1, 2, "23")]
        [DataRow(Operation.Substring, nameof(testClass.String), "123456789", 0, 1, "1")]
        [DataRow(Operation.Substring, nameof(testClass.String), "123456789", 7, 8, typeof(ArgumentOutOfRangeException))]
        [DataRow(Operation.Substring, nameof(testClass.String), "123456789", 10, 12, typeof(ArgumentOutOfRangeException))]
        [TestMethod] public void ComposeSubstringExpTest(Operation o, string property, string x, object y, object z, dynamic expected) {
            var param = Expression.Parameter(typeof(testClass), "x");
            var vy = ValueFactory.Create(random<string>(), y);
            var vz = ValueFactory.Create(random<string>(), z);
            var body = Expr.Compose(param, o, property, vy, vz);
            isNotNull(body);
            var obj = new testClass() { String = x };
            var expression = Expression.Lambda<Func<testClass, object>>(body, param);
            var actual = expression.Compile().Invoke(obj);
            var exception = expected as Type;
            if (exception is null) areEqual(expected, actual);
            else isInstanceOfType(actual, expected);
        }
        [TestMethod] public void ComposeTest() { }
    }
}
