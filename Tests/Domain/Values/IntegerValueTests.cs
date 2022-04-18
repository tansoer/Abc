using System;
using Abc.Aids.Random;
using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Values {
    [TestClass]
    public class IntegerValueTests : BaseNumericalValueTests<IntegerValue, int> {

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            f = GetRandom.Int32(-10000, 1000);
            t = GetRandom.Int32(f, 2000);
        }

        protected override void addTest() => assert(f, t, (decimal) (f + t));
        protected override void subtractTest() => assert(f, t, (decimal) (f - t));
        protected override void multiplyTest() => assert(f, t, ((decimal) f * t));
        protected override void divideTest() => assert(f, t, ((double) f / t));
        protected override void inverseTest() => assert(f, 1M / f);
        protected override void oppositeTest() => assert(f, (decimal) -f);
        protected override void squareTest() => assert(f, (double) f * f);
        protected override void sqrtTest() => assert(f * f, Math.Abs((double) f));
        protected override void powerTest() => assert(f, 3, ((double) f * f * f));

    }

}