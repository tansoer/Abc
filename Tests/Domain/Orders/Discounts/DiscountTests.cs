using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Products.Price;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Discounts {

    [TestClass]
    public class DiscountTests :AbstractTests<Discount, Entity<DiscountData>> {
        private IPrice testPrice;
        [TestInitialize] public override void TestInitialize() {
            base.TestInitialize();
            var d = random<PriceData>();
            var l = IsoCurrencies.Get;
            var idx = GetRandom.Int32(0, l.Count - 1);
            d.CurrencyId = l[idx].Id;
            testPrice = PriceFactory.Create(d);
        }
        private class testClass :Discount {
            public testClass(DiscountData d) : base(d) { }
        }
        protected override Discount createObject() => new testClass(GetRandom.ObjectOf<DiscountData>());
        [TestMethod] public void DiscountTypeIdTest() => isReadOnly(obj.Data.DiscountTypeId);
        [TestMethod] public void OrderEventIdTest() => isReadOnly(obj.Data.OrderEventId);
        [TestMethod]
        public async Task DiscountTypeTest()
            => await testItemAsync<DiscountTypeData, IDiscountType, IDiscountTypesRepo>(
                obj.DiscountTypeId, () => obj.DiscountType.Data, d => new DiscountType(d));
        [TestMethod]
        public void CalculateTest() {
            var d = obj.Calculate(testPrice);
            var amount = Archetype.get(testPrice?.Amount?.ToString());
            areEqual(0M, d.Data.Amount);
            validatePrice(d, testPrice, amount, obj);
        }

        internal static void validatePrice(IPrice d, IPrice testPrice, string amount, Discount obj) {
            isGuid(d.Id);
            areEqual(Archetype.get(testPrice?.Amount?.Currency?.Id), d.Data.CurrencyId);
            areEqual($"{obj.Code} for {amount}", d.Data.Code);
            areEqual($"{obj.Name} for {amount}", d.Data.Name);
            areEqual($"{obj.Details} for {amount}", d.Data.Details);
            areEqual(obj.Id, d.Data.DiscountId);
            areEqual((string)null, d.Data.PossiblePriceId);
            isTrue(d.Data.ValidFrom > DateTime.Now.AddSeconds(-1));
            isTrue(d.Data.ValidFrom < DateTime.Now);
            areEqual((DateTime?)null, d.Data.ValidTo);
            areEqual(testPrice.Id, d.Data.PriceId);
            areEqual((string)null, d.Data.ProductId);
            areEqual((string)null, d.Data.RuleSetId);
            areEqual((string)null, d.Data.RuleOverrideId);
            areEqual((string)null, d.Data.ProductTypeId);
            areEqual(PriceKind.Unspecified, d.Data.Kind);
        }

        [TestMethod]
        public void CalculatePriceTest() {
            var d = obj.calculatePrice(testPrice);
            areEqual(0M, d.Amount);
            validatePriceData(d, testPrice);
        }

        internal static void validatePriceData(PriceData d, IPrice testPrice) {
            isGuid(d.Id);
            areEqual(Archetype.get(testPrice?.Amount?.Currency?.Id), d.CurrencyId);
            areEqual((string)null, d.Code);
            areEqual((string)null, d.Name);
            areEqual((string)null, d.Details);
            areEqual((string)null, d.DiscountId);
            areEqual((string)null, d.PossiblePriceId);
            areEqual((DateTime?)null, d.ValidFrom);
            areEqual((DateTime?)null, d.ValidTo);
            areEqual((string)null, d.PriceId);
            areEqual((string)null, d.ProductId);
            areEqual((string)null, d.RuleSetId);
            areEqual((string)null, d.RuleOverrideId);
            areEqual((string)null, d.ProductTypeId);
            areEqual(PriceKind.Unspecified, d.Kind);
        }
    }
}