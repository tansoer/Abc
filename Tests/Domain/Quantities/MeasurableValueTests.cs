using System;
using Abc.Aids.Random;
using Abc.Core.Rounding;
using Abc.Data.Currencies;
using Abc.Domain.Currencies;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Quantities {
    [TestClass]
    public class MeasurableValueTests :
        AbstractTests<MeasurableValue<Money, decimal, Currency>, object> {
        private decimal amount;
        private Currency currency;
        private DateTime date;
        private class testClass :MeasurableValue<Money, decimal, Currency> {
            public testClass(decimal amount, Currency m, DateTime? date = null) : base(amount, m, date) { }
            public override int CompareTo(Money other) => 0;
            public override Money ConvertTo(Currency m) => null;
            public override Money Add(Money q) => null;
            public override Money Subtract(Money q) => null;
            public override Money Multiply(decimal a) => null;
            public override Money Divide(decimal a) => null;
            public override Money Round(IRoundingPolicy p) => null;
            public override bool IsEqual(Money q) => false;
            public override bool IsLess(Money q) => false;
            public override bool IsGreater(Money q) => false;
        }
        protected override MeasurableValue<Money, decimal, Currency> createObject() {
            amount = Convert.ToDecimal(GetRandom.Double(-1000, 1000));
            currency = new Currency(GetRandom.ObjectOf<CurrencyData>());
            date = GetRandom.DateTime(DateTime.Now.AddYears(-30), DateTime.Now);
            return new testClass(amount, currency, date);
        }
        [TestMethod] public void ConvertToTest() => isAbstractMethod(nameof(obj.ConvertTo));
        [TestMethod] public void AddTest() => isAbstractMethod(nameof(obj.Add));
        [TestMethod] public void SubtractTest() => isAbstractMethod(nameof(obj.Subtract));
        [TestMethod] public void MultiplyTest() => isAbstractMethod(nameof(obj.Multiply));
        [TestMethod] public void DivideTest() => isAbstractMethod(nameof(obj.Divide));
        [TestMethod] public void RoundTest() => isAbstractMethod(nameof(obj.Round));
        [TestMethod] public void IsEqualTest() => isAbstractMethod(nameof(obj.IsEqual));
        [TestMethod] public void IsLessTest() => isAbstractMethod(nameof(obj.IsLess));
        [TestMethod] public void IsGreaterTest() => isAbstractMethod(nameof(obj.IsGreater));
        [TestMethod] public void ToStringTest() => areEqual($"{amount} {currency.Code}", obj.ToString());
        [TestMethod] public void CompareToTest() { }
        [TestMethod] public void AmountTest() => areEqual(amount, obj.Amount);
        [TestMethod] public void ValidFromTest() => isProperty<DateTime?>();
        [TestMethod] public void ValidToTest() => isProperty<DateTime?>();
    }
}
