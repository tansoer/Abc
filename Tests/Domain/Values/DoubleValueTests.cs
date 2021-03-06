using System;
using Abc.Aids.Random;
using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Values {

    [TestClass]
    public class DoubleValueTests : BaseNumericalValueTests<DoubleValue, double> {

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            f = GetRandom.Double(-1000000, 1000000);
            t = GetRandom.Double(f, 2000000);
        }

        protected override void addTest() => assert(f, t, f + t);
        protected override void subtractTest() => assert(f, t, f - t);
        protected override void multiplyTest() => assert(f, t, f * t);
        protected override void divideTest() => assert(f, t, f / t);
        protected override void inverseTest() => assert(f, 1 / f);
        protected override void oppositeTest() => assert(f, -f);
        protected override void squareTest() => assert(f, f * f);
        protected override void sqrtTest() => assert(f * f, Math.Abs(f));
        protected override void powerTest() => assert(f, 3, f * f * f);

    }

}
