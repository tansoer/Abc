using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Orders.Discounts;
using Abc.Domain.Products.Price;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Price {
    [TestClass]
    public class PriceDiscountTests :SealedTests<PriceDiscount, BasePrice> {
        protected override PriceDiscount createObject() {
            var d = random<PriceData>();
            d.Kind = PriceKind.Discount;
            return PriceFactory.Create(d) as PriceDiscount;
        }
        [TestMethod]
        public void DiscountIdTest() => isReadOnly(obj.Data.DiscountId, true);
        [TestMethod]
        public void PriceIdTest() => isReadOnly(obj.Data.PriceId, true);
        [TestMethod]
        public async Task DiscountTest() => await testItemAsync<DiscountData, IDiscount, IDiscountsRepo>(
            obj.discountId, () => obj.Discount.Data, DiscountFactory.Create);
        [TestMethod]
        public async Task PriceTest() => await testItemAsync<PriceData, IPrice, IPricesRepo>(
            obj.priceId, () => obj.Price.Data, PriceFactory.Create);
    }
}