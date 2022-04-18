using System;
using System.Reflection;
using Abc.Aids.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Classes {
    [TestClass]
    public class NonPublicFlagsForTests : TestsBase {

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(NonPublicFlagsFor);
            testType = typeof(testClass);
        }

        private const BindingFlags p = BindingFlags.NonPublic;
        private const BindingFlags i = BindingFlags.Instance;
        private const BindingFlags s = BindingFlags.Static;
        private const BindingFlags d = BindingFlags.DeclaredOnly;
        private Type testType;

        internal class testClass {

            public void Aaa() => bbb();

            private void bbb() { }

            public static void Ccc() => ddd();

            private static void ddd() { }

        }

        [TestMethod]
        public void AllTest()
            => testMembers(i | s | p, NonPublicFlagsFor.All, 4);

        [TestMethod]
        public void InstanceTest()
            => testMembers(i | p, NonPublicFlagsFor.Instance, 3);

        [TestMethod]
        public void StaticTest()
            => testMembers(s | p, NonPublicFlagsFor.Static, 1);

        [TestMethod]
        public void DeclaredTest()
            => testMembers(d | i | s | p, NonPublicFlagsFor.Declared, 2);

        private void testMembers(BindingFlags expected, BindingFlags actual,
            int membersCount) {
            var a = testType.GetMembers(actual);
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(membersCount, a.Length);
        }

    }
}
