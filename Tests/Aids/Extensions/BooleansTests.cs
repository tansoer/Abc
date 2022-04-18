using Abc.Aids.Extensions;
using Abc.Aids.Random;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Aids.Extensions {

    [TestClass]
    public class BooleansTests : TestsBase {

        private static bool f => false;
        private static bool t => true;
        private static string fs => "False";
        private static string ts => "True";

        [TestInitialize]
        public void TestInitialize() {
            type = typeof(Booleans);
        }

        [TestMethod]
        public void ToBooleanTest() {
            Assert.AreEqual(f, Booleans.ToBoolean(null));
            Assert.AreEqual(f, Booleans.ToBoolean(rndStr));
            Assert.AreEqual(f, Booleans.ToBoolean(f.ToString()));
            Assert.AreEqual(t, Booleans.ToBoolean(t.ToString()));
            Assert.AreEqual(f, Booleans.ToBoolean("f"));
            Assert.AreEqual(t, Booleans.ToBoolean("t"));
            Assert.AreEqual(f, Booleans.ToBoolean("no"));
            Assert.AreEqual(t, Booleans.ToBoolean("yes"));
            Assert.AreEqual(f, Booleans.ToBoolean("n"));
            Assert.AreEqual(t, Booleans.ToBoolean("y"));
        }

        [TestMethod]
        public void ToBooleanStringTest() {
            Assert.AreEqual(null, Booleans.ToBooleanString(null));
            var s = rndStr;
            Assert.AreEqual(s, Booleans.ToBooleanString(s));
            Assert.AreEqual(fs, Booleans.ToBooleanString(f.ToString()));
            Assert.AreEqual(ts, Booleans.ToBooleanString(t.ToString()));
            Assert.AreEqual(fs, Booleans.ToBooleanString("f"));
            Assert.AreEqual(ts, Booleans.ToBooleanString("t"));
            Assert.AreEqual(fs, Booleans.ToBooleanString("no"));
            Assert.AreEqual(ts, Booleans.ToBooleanString("yes"));
            Assert.AreEqual(fs, Booleans.ToBooleanString("n"));
            Assert.AreEqual(ts, Booleans.ToBooleanString("y"));
        }

        [TestMethod]
        public void IsFalseStringTest() {
            Assert.AreEqual(f, Booleans.IsFalseString(null));
            Assert.AreEqual(f, Booleans.IsFalseString(rndStr));
            Assert.AreEqual(t, Booleans.IsFalseString(f.ToString()));
            Assert.AreEqual(f, Booleans.IsFalseString(t.ToString()));
            Assert.AreEqual(t, Booleans.IsFalseString("f"));
            Assert.AreEqual(f, Booleans.IsFalseString("t"));
            Assert.AreEqual(t, Booleans.IsFalseString("no"));
            Assert.AreEqual(f, Booleans.IsFalseString("yes"));
            Assert.AreEqual(t, Booleans.IsFalseString("n"));
            Assert.AreEqual(f, Booleans.IsFalseString("y"));
        }

        [TestMethod]
        public void IsTrueStringTest() {
            Assert.AreEqual(false, Booleans.IsTrueString(null));
            Assert.AreEqual(false, Booleans.IsTrueString(rndStr));
            Assert.AreEqual(false, Booleans.IsTrueString(false.ToString()));
            Assert.AreEqual(true, Booleans.IsTrueString(true.ToString()));
            Assert.AreEqual(false, Booleans.IsTrueString("f"));
            Assert.AreEqual(true, Booleans.IsTrueString("t"));
            Assert.AreEqual(false, Booleans.IsTrueString("no"));
            Assert.AreEqual(true, Booleans.IsTrueString("yes"));
            Assert.AreEqual(false, Booleans.IsTrueString("n"));
            Assert.AreEqual(true, Booleans.IsTrueString("y"));
        }

        [TestMethod]
        public void ToStringTest() {
            Assert.AreEqual("True", Booleans.ToString(true));
            Assert.AreEqual("False", Booleans.ToString(false));
        }

        [TestMethod]
        public void AndTest() {
            Assert.AreEqual(true, true.And(true));
            Assert.AreEqual(false, false.And(true));
            Assert.AreEqual(false, true.And(false));
            Assert.AreEqual(false, false.And(false));
        }

        [TestMethod]
        public void AddTest() {
            Assert.AreEqual(true.Or(true), true.Add(true));
            Assert.AreEqual(false.Or(true), false.Add(true));
            Assert.AreEqual(true.Or(false), true.Add(false));
            Assert.AreEqual(false.Or(false), false.Add(false));
        }

        [TestMethod]
        public void MultiplyTest() {
            Assert.AreEqual(true.And(true), true.Multiply(true));
            Assert.AreEqual(false.And(true), false.Multiply(true));
            Assert.AreEqual(true.And(false), true.Multiply(false));
            Assert.AreEqual(false.And(false), false.Multiply(false));
        }

        [TestMethod]
        public void OrTest() {
            Assert.AreEqual(true, true.Or(true));
            Assert.AreEqual(true, false.Or(true));
            Assert.AreEqual(true, true.Or(false));
            Assert.AreEqual(false, false.Or(false));
        }

        [TestMethod]
        public void XorTest() {
            Assert.AreEqual(false, true.Xor(true));
            Assert.AreEqual(true, false.Xor(true));
            Assert.AreEqual(true, true.Xor(false));
            Assert.AreEqual(false, false.Xor(false));
        }

        [TestMethod]
        public void NotTest() {
            Assert.AreEqual(false, true.Not());
            Assert.AreEqual(true, false.Not());
        }

    }

}