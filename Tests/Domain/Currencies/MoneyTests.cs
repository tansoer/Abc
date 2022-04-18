using System;
using Abc.Aids.Constants;
using Abc.Aids.Random;
using Abc.Core.Rounding;
using Abc.Data.Currencies;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Quantities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Currencies {

    [TestClass]
    public class MoneyTests : SealedTests<Money, MeasurableValue<Money, decimal, Currency>> {

        private decimal greaterAmount;
        private decimal lessAmount;
        private decimal sameAmount;
        private DateTime date;
        private Currency fromCurrency;
        private Currency toCurrency;
        private Money greaterMoney;
        private Money lessMoney;
        private Money sameMoney;

        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            date = GetRandom.DateTime(DateTime.Now.AddYears(-30), DateTime.Now);
            createAmounts();
            createCurrencies();
        }

        [TestMethod]
        public void AddTest() {
            var x = obj.Add(greaterMoney).Round(new RoundingPolicy(RoundingStrategy.Round, 10));
            Assert.AreEqual(greaterMoney.Currency.Id, x.Currency.Id);
            Assert.AreEqual(obj.ValidFrom, x.ValidFrom);
            var d1 = obj.Currency.ToBase(obj.Amount, obj.ValidFrom);
            var d2 = greaterMoney.Currency.ToBase(greaterMoney.Amount, obj.ValidFrom);
            var a = greaterMoney.Currency.FromBase(d1 + d2, obj.ValidFrom);
            a = new RoundingPolicy(RoundingStrategy.Round, 10).Round(a);
            Assert.AreEqual(a, x.Amount);
        }

        [TestMethod]
        public void CompareToTest() {
            Assert.AreEqual(0, obj.CompareTo(obj), $"{obj} != {obj}");
            Assert.AreEqual(0, obj.CompareTo(sameMoney), $"{obj}={sameMoney} != {sameMoney}");
            Assert.AreEqual(1, obj.CompareTo(lessMoney), $"{obj}={sameMoney} < {lessMoney}");
            Assert.AreEqual(-1, obj.CompareTo(greaterMoney), $"{obj}={sameMoney} > {greaterMoney}");
        }

        [TestMethod]
        public void ConvertToCurrancyThatHasNoRateReturnsZeroAmountTest() {
            createCurrencies(false);
            Assert.AreEqual(fromCurrency.Id, obj.Currency.Id);
            Assert.AreEqual(toCurrency.Id, sameMoney.Currency.Id);
            Assert.AreEqual(toCurrency.Id, greaterMoney.Currency.Id);
            Assert.AreEqual(toCurrency.Id, lessMoney.Currency.Id);
            Assert.AreEqual(sameAmount, obj.Amount);
            Assert.AreEqual(0M, sameMoney.Amount);
            Assert.AreEqual(0M, greaterMoney.Amount);
            Assert.AreEqual(0M, lessMoney.Amount);
        }

        [TestMethod]
        public void ConvertToTest() {
            obj = new Money(GetRandom.Decimal(1, 1000000000000), fromCurrency, date);
            var o = obj.ConvertTo(toCurrency);
            Assert.AreEqual(toCurrency.Id, o.Currency.Id);
            allPropertiesAreEqual(toCurrency.Data, o.Currency.Data);
            Assert.AreEqual(obj.Amount * fromCurrency.Rate(date) / toCurrency.Rate(date), o.Amount);
        }

        [TestMethod]
        public void CurrencyTest() {
            obj = new Money(GetRandom.Decimal(), fromCurrency.Id);
            Assert.AreEqual(fromCurrency.Id, obj.Currency.Id);
            allPropertiesAreEqual(fromCurrency.Data, obj.Currency.Data);
        }

        [TestMethod]
        public void DivideTest() {
            testDivide(sameAmount, obj.Amount / sameAmount, fromCurrency.Id);
            testDivide(0, 0, Word.Unspecified);
            testDivide(decimal.MaxValue, obj.Amount / decimal.MaxValue, fromCurrency.Id);
            testDivide(decimal.MinValue, obj.Amount / decimal.MinValue, fromCurrency.Id);
        }

        [TestMethod]
        public void IsEqualTest() {
            Assert.IsTrue(obj.IsEqual(sameMoney));
            Assert.IsFalse(obj.IsEqual(greaterMoney));
            Assert.IsFalse(obj.IsEqual(lessMoney));
        }

        [TestMethod]
        public void IsGreaterTest() {
            Assert.IsFalse(obj.IsGreater(sameMoney));
            Assert.IsFalse(obj.IsGreater(greaterMoney), $"{obj}={sameMoney} > {greaterMoney}");
            Assert.IsTrue(obj.IsGreater(lessMoney), $"{obj}={sameMoney} < {lessMoney}");
        }

        [TestMethod]
        public void IsLessTest() {
            Assert.IsFalse(obj.IsLess(sameMoney));
            Assert.IsTrue(obj.IsLess(greaterMoney), $"{obj}={sameMoney} > {greaterMoney}");
            Assert.IsFalse(obj.IsLess(lessMoney), $"{obj}={sameMoney} < {lessMoney}");
        }

        [TestMethod]
        public void MultiplyTest() {
            testMultiply(sameAmount, obj.Amount * sameAmount, fromCurrency.Id);
            testMultiply(0, 0, fromCurrency.Id);
            testMultiply(decimal.MaxValue, 0, Word.Unspecified);
            testMultiply(decimal.MinValue, 0, Word.Unspecified);
        }

        [TestMethod]
        public void ParseTest() {
            var s = $"{obj.Amount} {obj.Currency.Id}";
            var money = Money.Parse(s);
            Assert.AreEqual(obj.Amount, money.Amount);
            Assert.AreEqual(obj.Currency.Id, money.Currency.Id);
            money = Money.Parse("bla bla");
            Assert.AreEqual(0, money.Amount);
            Assert.AreEqual(Word.Unspecified, money.Currency.Id);
        }

        [TestMethod]
        public void RoundTest() {
            roundTest(4.5, new RoundingPolicy(RoundingStrategy.RoundUp, 1), 4.45);
            roundTest(4.46, new RoundingPolicy(RoundingStrategy.RoundUp, 2), 4.456);
            roundTest(-4.5, new RoundingPolicy(RoundingStrategy.RoundUp, 1), -4.45);
            roundTest(-4.46, new RoundingPolicy(RoundingStrategy.RoundUp, 2), -4.456);
            roundTest(1400, new RoundingPolicy(RoundingStrategy.RoundUp, 2), 1400.00);
            roundTest(4.4, new RoundingPolicy(RoundingStrategy.RoundDown, 1), 4.45);
            roundTest(4.45, new RoundingPolicy(RoundingStrategy.RoundDown, 2), 4.456);
            roundTest(-4.4, new RoundingPolicy(RoundingStrategy.RoundDown, 1), -4.45);
            roundTest(-4.45, new RoundingPolicy(RoundingStrategy.RoundDown, 2), -4.456);
            roundTest(1400, new RoundingPolicy(RoundingStrategy.RoundDown, 1), 1400.00);
            roundTest(4.5, new RoundingPolicy(RoundingStrategy.Round, 1, 5), 4.45);
            roundTest(4.45, new RoundingPolicy(RoundingStrategy.Round, 2, 7), 4.456);
            roundTest(-4.5, new RoundingPolicy(RoundingStrategy.Round, 1, 5), -4.45);
            roundTest(-4.45, new RoundingPolicy(RoundingStrategy.Round, 2, 7), -4.456);
            roundTest(0.01, new RoundingPolicy(RoundingStrategy.Round, 5, 5), 0.01);
            roundTest(0.01, new RoundingPolicy(RoundingStrategy.Round, 5, 5), 0.0100000000002);
            roundTest(1, new RoundingPolicy(RoundingStrategy.Round, 0, 5), 0.99999999999999989);
            roundTest(4.5, new RoundingPolicy(RoundingStrategy.RoundUpByStep, 0.25), 4.45);
            roundTest(-4.5, new RoundingPolicy(RoundingStrategy.RoundUpByStep, 0.25), -4.45);
            roundTest(4.25, new RoundingPolicy(RoundingStrategy.RoundDownByStep, 0.25), 4.45);
            roundTest(-4.25, new RoundingPolicy(RoundingStrategy.RoundDownByStep, 0.25), -4.45);
            roundTest(4.5, new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 1), 4.45);
            roundTest(4.46, new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 2), 4.456);
            roundTest(-4.4, new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 1), -4.45);
            roundTest(-4.45, new RoundingPolicy(RoundingStrategy.RoundTowardsPositive, 2), -4.456);
            roundTest(4.4, new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 1), 4.45);
            roundTest(4.45, new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 2), 4.456);
            roundTest(-4.5, new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 1), -4.45);
            roundTest(-4.46, new RoundingPolicy(RoundingStrategy.RoundTowardsNegative, 2), -4.456);
        }

        [TestMethod]
        public void SubtractTest() {
            var x = obj.Subtract(greaterMoney).Round(new RoundingPolicy(RoundingStrategy.Round, 10));
            Assert.AreEqual(greaterMoney.Currency.Id, x.Currency.Id);
            Assert.AreEqual(obj.ValidFrom, x.ValidFrom);
            var d1 = obj.Currency.ToBase(obj.Amount, obj.ValidFrom);
            var d2 = greaterMoney.Currency.ToBase(greaterMoney.Amount, obj.ValidFrom);
            var a = greaterMoney.Currency.FromBase(d1 - d2, obj.ValidFrom);
            a = new RoundingPolicy(RoundingStrategy.Round, 10).Round(a);
            Assert.AreEqual(a, x.Amount);
        }

        [TestMethod]
        public void TryParseTest() {
            var s = $"{obj.Amount} {obj.Currency.Id}";
            var b = Money.TryParse(s, out var money);
            Assert.IsTrue(b);
            Assert.AreEqual(obj.Amount, money.Amount);
            Assert.AreEqual(obj.Currency.Id, money.Currency.Id);
            b = Money.TryParse("bla bla", out money);
            Assert.IsFalse(b);
            Assert.AreEqual(0, money.Amount);
            Assert.AreEqual(Word.Unspecified, money.Currency.Id);
        }

        [TestMethod]
        public void UnspecifiedTest() {
            obj = Money.Unspecified;
            Assert.AreEqual(0, obj.Amount);
            Assert.AreEqual(Word.Unspecified, obj.Currency.Id);
        }

        private void createAmounts() {
            var d = GetRandom.Double(1000, 1000000);
            sameAmount = Convert.ToDecimal(d);
            greaterAmount = Convert.ToDecimal(GetRandom.Double(d + 100, 1000000));
            lessAmount = Convert.ToDecimal(GetRandom.Double(0.0000001, d - 100));
        }

        private void createCurrencies(bool hasRates = true) {
            fromCurrency = createCurrency(hasRates);
            toCurrency = createCurrency(hasRates);
            obj = new Money(sameAmount, fromCurrency, date);
            sameMoney = obj.ConvertTo(toCurrency);
            greaterMoney = new Money(greaterAmount, fromCurrency, date).ConvertTo(toCurrency);
            lessMoney = new Money(lessAmount, fromCurrency, date).ConvertTo(toCurrency);
        }

        private Currency createCurrency(bool hasRates = true) {
            var cur = new Currency(GetRandom.ObjectOf<CurrencyData>());
            if (hasRates) CurrencyTests.addRandomRates(cur, date);
            GetRepo.Instance<ICurrencyRepo>().Add(cur);

            return cur;
        }

        private void roundTest(double expected, RoundingPolicy p, double a) {
            sameAmount = Convert.ToDecimal(a);
            var e = Convert.ToDecimal(expected);
            obj = new Money(sameAmount, fromCurrency);
            var q = obj.Round(p);
            Assert.AreNotSame(obj, q);
            Assert.AreEqual(e, q.Amount);
            allPropertiesAreEqual(fromCurrency.Data, q.Currency.Data);
        }
        private void testDivide(in decimal a, decimal expected, string currencyId) {
            var x = obj.Divide(a);
            Assert.AreEqual(currencyId, x.Currency.Id);
            Assert.AreEqual(expected, x.Amount);

            if (currencyId != fromCurrency.Id) return;
            allPropertiesAreEqual(fromCurrency.Data, x.Currency.Data);
        }
        private void testMultiply(in decimal a, decimal expected, string currencyId) {
            var x = obj.Multiply(a);
            Assert.AreEqual(currencyId, x.Currency.Id);
            Assert.AreEqual(expected, x.Amount);

            if (currencyId != fromCurrency.Id) return;
            allPropertiesAreEqual(fromCurrency.Data, x.Currency.Data);
        }
        [TestMethod] public void ToStringTest() 
            => areEqual($"{obj.Amount} {obj.Currency}", obj.ToString());
    }
}