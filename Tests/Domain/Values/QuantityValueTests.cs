using System;
using Abc.Aids.Random;
using Abc.Core.Rounding;
using Abc.Data.Quantities;
using Abc.Domain.Common;
using Abc.Domain.Quantities;
using Abc.Domain.Values;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Values {

    [TestClass]
    public class QuantityValueTests : BaseNumericalValueTests<QuantityValue, Quantity> {

        private static Quantity randomQuantity() => new Quantity(randomDouble(), randomUnit());
        private static Unit randomUnit() => new FactoredUnit(data);
        private static UnitData data => new UnitData { Id = "c", Name = "c", Code = "c", Details = "c", UnitType = UnitType.Factored };
        private static UnitFactorData factorData =>
            new UnitFactorData { UnitId = "c", SystemOfUnitsId = "SI", Factor = 1 };


        private static double randomDouble() => GetRandom.Double(-1000000, 1000000);
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            GetRepo.Instance<IUnitsRepo>().Add(randomUnit());
            GetRepo.Instance<IUnitFactorsRepo>().Add(new UnitFactor(factorData));
            f = randomQuantity();
            t = randomQuantity();

            if (f.Amount <= t.Amount) return;
            var d = f;
            f = t;
            t = d;
        }

        [TestMethod] public void UnitIdTest() => isReadOnly(obj.Data.UnitOrCurrencyId);

        protected void assert(Quantity a, Quantity b, Quantity expected) {
            var x = varX(a);
            var y = varY(b);
            var name = $"(({a}) {functionSign} ({b}))";
            var z = binaryFunction(x, y);
            Assert.IsNotNull(z);
            Assert.AreEqual(name, z.Name);
            Assert.AreEqual(round(expected.Amount), round(((Quantity) z.GetValue()).Amount));
            Assert.AreEqual(expected.Unit.Id, ((Quantity) z.GetValue()).Unit.Id);
        }
        internal static double round(double d)
            => new RoundingPolicy(RoundingStrategy.Round, 10).Round(d);

        protected void assert(Quantity a, Quantity expected) {
            var x = varX(a);
            var name = $"({functionSign} ({a}))";
            var z = unaryFunction(x);
            Assert.IsNotNull(z);
            Assert.AreNotEqual(double.NaN, x.Value.Amount);
            Assert.AreEqual(name, z.Name);
            Assert.AreEqual(round(expected.Amount), round(((Quantity) z.GetValue()).Amount));
            Assert.AreEqual(expected.Unit.Id, ((Quantity) z.GetValue()).Unit.Id);
        }
        protected void assert(Quantity a, int b, Quantity expected) {
            var x = varX(a);
            var y = varY(b);
            var name = $"(({a}) {functionSign} ({b}))";
            var z = binaryFunction(x, y);
            Assert.IsNotNull(z);
            Assert.AreEqual(name, z.Name);
            Assert.AreEqual(round(expected.Amount), round(((Quantity) z.GetValue()).Amount));
            Assert.AreEqual(expected.Unit.Id, ((Quantity) z.GetValue()).Unit.Id);
        }
        protected override void addTest() => assert(f, t, f.Add(t));
        protected override void subtractTest() => assert(f, t, f.Subtract(t));
        protected override void multiplyTest() => assert(f, t, f.Multiply(t));
        protected override void divideTest() => assert(f, t, f.Divide(t));
        protected override void inverseTest() => assert(f, f.Inverse());
        protected override void oppositeTest() => assert(f, new Quantity(-f.Amount, f.Unit));
        protected override void squareTest() => assert(f, f.Multiply(f));
        protected override void sqrtTest()
            => assert(new Quantity(Math.Abs(f.Amount), f.Unit), new Quantity(Math.Sqrt(Math.Abs(f.Amount)), f.Unit.Sqrt()));
        protected override void powerTest() => assert(f, 3, f.Power(3));


    }

}