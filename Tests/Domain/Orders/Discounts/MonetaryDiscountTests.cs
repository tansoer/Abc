using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Products.Price;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Discounts {
    [TestClass]
    public class MonetaryDiscountTests :SealedTests<MonetaryDiscount, Discount> {
        private IPrice testPrice;
        private Money testAmount;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            var d = random<PriceData>();
            var l = IsoCurrencies.Get;
            var idx = GetRandom.Int32(0, l.Count - 1);
            d.CurrencyId = l[idx].Id;
            testPrice = PriceFactory.Create(d);
            testAmount = obj.Amount.ConvertTo(testPrice?.Amount?.Currency);
        }
        protected override MonetaryDiscount createObject() {
            var d = random<DiscountData>();
            d.DiscountType = DiscountsType.Monetary;
            return DiscountFactory.Create(d) as MonetaryDiscount;
        }
        [TestMethod]
        public void CalculateTest() {
            var d = obj.Calculate(testPrice);
            var amount = Archetype.get(testPrice?.Amount?.ToString());
            areEqual(testAmount.Amount, d.Data.Amount);
            DiscountTests.validatePrice(d, testPrice, amount, obj);
        }
        [TestMethod]
        public void CalculatePriceTest() {
            var d = obj.calculatePrice(testPrice);
            areEqual(testAmount.Amount, d.Amount);
            DiscountTests.validatePriceData(d, testPrice);
        }
   }
}