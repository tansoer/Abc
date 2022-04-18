using Abc.Aids.Random;
using Abc.Data.Currencies;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Currencies;
using Abc.Domain.Products.Price;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Domain.Products.Price {

    [TestClass] public class BasePriceTests : 
        AbstractTests<BasePrice, Entity<PriceData>> {
        private class testClass :BasePrice {
            public testClass(PriceData d = null) : base(d) { }
        }
        protected override BasePrice createObject() {
            var d = GetRandom.ObjectOf<PriceData>();
            var ud = GetRandom.ObjectOf<CurrencyData>();
            ud.Id = d.CurrencyId;
            GetRepo.Instance<ICurrencyRepo>().Add(new Currency(ud));
            return new testClass(d);
        }
        [TestMethod] public void DecimalAmountTest() => 
            Assert.AreEqual(obj.amount, obj.Data.Amount);
        [TestMethod] public void CurrencyIdTest() =>
            Assert.AreEqual(obj.currencyId, obj.Data.CurrencyId);
        [TestMethod] public void AmountTest() {
            isReadOnly();
            Assert.AreEqual(obj.amount, obj.Amount.Amount);
            Assert.AreEqual(obj.currencyId, obj.Amount.Currency.Id);
            Assert.AreEqual(obj.ValidFrom, obj.Amount.ValidFrom);
        }
    }
}