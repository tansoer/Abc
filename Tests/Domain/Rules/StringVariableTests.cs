using Abc.Aids.Random;
using Abc.Domain.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Rules {

    [TestClass]
    public class StringVariableTests : BaseVariableTests<StringVariable, string> {

        private string f;
        private string t;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            f = "0000" + rndStr;
            t = rndStr;
        }

        protected override void isEqualTest() {
            assert(f, f, true);
            assert(f, t, false);
            assert(t, f, false);
            assert(f, f, true);
        }
        protected override void isGreaterTest() {
            assert(t, t, false);
            assert(t, f, true);
            assert(f, t, false);
            assert(f, f, false);
        }
        protected override void isLessTest() {
            assert(t, t, false);
            assert(t, f, false);
            assert(f, t, true);
            assert(f, f, false);
        }
        protected override void addTest() => assert(f, t, f + t);
        protected override void trimTest() => assert("      " + f + "    ", f);
        protected override void toLowerTest() => assert(f.ToUpper(), f.ToLower());
        protected override void toUpperTest() => assert(f.ToLower(), f.ToUpper());
        protected override void startsWithTest() {
            assert(f, t, false);
            assert(t + f, t, true);
        }
        protected override void endsWithTest() {
            assert(f, t, false);
            assert(f + t, t, true);
        }
        protected override void containsTest() {
            assert(f, t, false);
            assert(f + t, t, true);
            assert(t + f, t, true);
            assert(f + t + f, t, true);
        }
        protected override void getLengthTest() {
            assert(f, f.Length);
            assert(t, t.Length);
            assert(t + f, t.Length + f.Length);
        }
        protected override void substringToEndTest() {
            assert(f + t, f.Length, t);
            assert(t + f, t.Length, f);
        }
        protected override void substringTest() {
            assert(f + t + f, f.Length, t.Length, t);
            assert(t + f + t, t.Length, f.Length, f);
        }

    }

}