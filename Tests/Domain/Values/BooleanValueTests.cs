using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Values {

    [TestClass]
    public class BooleanValueTests : BaseValueTests<BooleanValue, bool> {

        protected override void andTest() {
            assert(true, true, true);
            assert(true, false, false);
            assert(false, true, false);
            assert(false, false, false);
        }

        protected override void orTest() {
            assert(true, true, true);
            assert(true, false, true);
            assert(false, true, true);
            assert(false, false, false);
        }
        protected override void xorTest() {
            assert(true, true, false);
            assert(true, false, true);
            assert(false, true, true);
            assert(false, false, false);
        }
        protected override void notTest() {
            assert(true, false);
            assert(false, true);
        }
        protected override void isEqualTest() {
            assert(true, true, true);
            assert(true, false, false);
            assert(false, true, false);
            assert(false, false, true);
        }
        protected override void isGreaterTest() {
            assert(true, true, false);
            assert(true, false, true);
            assert(false, true, false);
            assert(false, false, false);
        }
        protected override void isLessTest() {
            assert(true, true, false);
            assert(true, false, false);
            assert(false, true, true);
            assert(false, false, false);
        }

        protected override void addTest() => orTest();

        protected override void multiplyTest() => andTest();
    }

}
