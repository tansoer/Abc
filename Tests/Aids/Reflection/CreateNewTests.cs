using Abc.Aids.Enums;
using Abc.Aids.Random;
using Abc.Aids.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Reflection {

    [TestClass]
    public class CreateNewTests : TestsBase {

        [TestInitialize] public void TestInitialize() => type = typeof(CreateNew);

        [TestMethod]
        public void InstanceTest() {
            var o1 = CreateNew.Instance<testClass1>();
            var o2 = CreateNew.Instance<testClass1>();
            Assert.IsInstanceOfType(o1, typeof(testClass1));
            Assert.AreNotEqual(o1.S, o2.S);
            Assert.AreNotEqual(o1.I, o2.I);
        }

        [TestMethod]
        public void CreateDefaultTest() {
            var o1 = CreateNew.Instance<testClass2>();
            var o2 = CreateNew.Instance<testClass2>();
            Assert.IsInstanceOfType(o1, typeof(testClass2));
            Assert.AreNotEqual(o1.S, o2.S);
            Assert.AreNotEqual(o1.I, o2.I);
        }

        [TestMethod]
        public void CreateStringTest() {
            var s = CreateNew.Instance<string>();
            Assert.IsInstanceOfType(s, typeof(string));
        }

        [TestMethod]
        public void CreateEnumTest() {
            var s = CreateNew.Instance<IsoGender>();
            Assert.IsInstanceOfType(s, typeof(IsoGender));
        }

        [TestMethod]
        public void CantCreateStaticTest() {
            //type argument cant be static
            //var s = Create.Instance<GetRandom>();
        }

        private class testClass1 {

            public testClass1(int i, string s) {
                S = s;
                I = i;
            }

            public int I { get; }
            public string S { get; }

        }

        private class testClass2 {

            public int I { get; } = GetRandom.Int32();
            public string S { get; } = rndStr;

        }

    }

}





