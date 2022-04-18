using System;
using Abc.Aids.Methods;
using Abc.Aids.Random;
using Abc.Aids.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Values {

    [TestClass] public class CompareTests : TestsBase {
        [TestInitialize] public void TestInitialize() => type = typeof(Compare);
        [TestMethod] public void IsGreaterTest() => doTest(testGreater);
        [TestMethod] public void IsEqualTest() => doTest(testEqual);
        [TestMethod] public void IsLessTest() => doTest(testLess);
        [TestMethod] public void IsNotGreaterTest() => doTest(testNotGreater);
        [TestMethod] public void IsNotEqualTest() => doTest(testNotEqual);
        [TestMethod] public void IsNotLessTest() => doTest(testNotLess);
        private static void doTest(Action<IComparable, IComparable> action) {
            action(true, false);
            action(rndStr, rndStr);
            action(GetRandom.Int8(max: 0), GetRandom.Int8(1));
            action(GetRandom.Int16(), GetRandom.Int16());
            action(GetRandom.Int32(), GetRandom.Int32());
            action(GetRandom.Int64(), GetRandom.Int64());
            action(GetRandom.UInt8(max: 100), GetRandom.UInt8(101));
            action(GetRandom.UInt16(), GetRandom.UInt16());
            action(GetRandom.UInt32(), GetRandom.UInt32());
            action(GetRandom.UInt64(), GetRandom.UInt64());
            action(GetRandom.Float(), GetRandom.Float());
            action(GetRandom.Double(), GetRandom.Double());
            action(GetRandom.Decimal(), GetRandom.Decimal());
            action(rndDt, rndDt);
            action(GetRandom.TimeSpan(), GetRandom.TimeSpan());
        }
        private static void testGreater<T>(T x, T y) where T : notnull, IComparable {
            while (x.Equals(y)) y = GetRandom.ObjectOf<T>();
            Sort.Descending(ref x, ref y);
            areEqual(true, x.IsGreater(y));
            areEqual(false, x.IsGreater(x));
            areEqual(false, y.IsGreater(x));
        }
        private static void testNotGreater<T>(T x, T y) where T : notnull, IComparable {
            while (x.Equals(y)) y = GetRandom.ObjectOf<T>();
            Sort.Descending(ref x, ref y);
            areEqual(false, x.IsNotGreater(y));
            areEqual(true, x.IsNotGreater(x));
            areEqual(true, y.IsNotGreater(x));
        }
        private static void testLess<T>(T x, T y) where T : notnull, IComparable {
            while (x.Equals(y)) y = GetRandom.ObjectOf<T>();
            Sort.Ascending(ref x, ref y);
            areEqual(true, x.IsLess(y));
            areEqual(false, x.IsLess(x));
            areEqual(false, y.IsLess(x));
        }
        private static void testNotLess<T>(T x, T y) where T : notnull, IComparable {
            while (x.Equals(y)) y = GetRandom.ObjectOf<T>();
            Sort.Ascending(ref x, ref y);
            areEqual(false, x.IsNotLess(y));
            areEqual(true, x.IsNotLess(x));
            areEqual(true, y.IsNotLess(x));
        }
        private static void testEqual<T>(T x, T y) where T : notnull, IComparable {
            areEqual(true, x.IsEqual(x));
            areEqual(false, x.IsEqual(y));
        }
        private static void testNotEqual<T>(T x, T y) where T : notnull, IComparable {
            areEqual(true, x.IsNotEqual(y));
            areEqual(false, x.IsNotEqual(x));
        }
    }
}
