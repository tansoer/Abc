using System;
using Abc.Aids.Methods;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Methods {

    [TestClass]
    public class SortTests : TestsBase {

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(Sort);
        }

        [TestMethod]
        public void AscendingTest() {
            sortAscendingTest(DateTime.MaxValue, DateTime.MinValue);
            sortAscendingTest(double.MaxValue, double.MinValue);
            sortAscendingTest(int.MaxValue, int.MinValue);
        }

        private static void sortAscendingTest<T>(T max, T min) where T : IComparable {
            Assert.IsTrue(max.CompareTo(min) >= 0);
            Sort.Ascending(ref max, ref min);
            Assert.IsTrue(max.CompareTo(min) <= 0);
        }
        [TestMethod]
        public void DescendingTest() {
            sortDescendingTest(DateTime.MinValue, DateTime.MaxValue);
            sortDescendingTest(double.MinValue, double.MaxValue);
            sortDescendingTest(int.MinValue, int.MaxValue);
        }

        private static void sortDescendingTest<T>(T min, T max) where T : IComparable {
            Assert.IsTrue(min.CompareTo(max) <= 0);
            Sort.Descending(ref min, ref max);
            Assert.IsTrue(min.CompareTo(max) >= 0);
        }

    }

}


