using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Products.Price;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Orders.Discounts {

    [TestClass]
    public class UnspecifiedDiscountTests :SealedTests<UnspecifiedDiscount, Discount> {
        private IPrice testPrice;
        [TestInitialize]
        public override void TestInitialize() {
            base.TestInitialize();
            var d = random<PriceData>();
            var l = IsoCurrencies.Get;
            var idx = GetRandom.Int32(0, l.Count - 1);
            d.CurrencyId = l[idx].Id;
            testPrice = PriceFactory.Create(d);
        }
        protected override UnspecifiedDiscount createObject() {
            var d = random<DiscountData>();
            d.DiscountType = DiscountsType.Unspecified;
            return DiscountFactory.Create(d) as UnspecifiedDiscount;
        }
        [TestMethod]
        public void CalculateTest() {
            var d = obj.Calculate(testPrice);
            var amount = Archetype.get(testPrice?.Amount?.ToString());
            areEqual(0M, d.Data.Amount);
            DiscountTests.validatePrice(d, testPrice, amount, obj);
        }
        [TestMethod]
        public void CalculatePriceTest() {
            var d = obj.calculatePrice(testPrice);
            areEqual(0M, d.Amount);
            DiscountTests.validatePriceData(d, testPrice);
        }
    }
}