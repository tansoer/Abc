using Abc.Aids.Random;
using Abc.Data.Products;
using Abc.Domain.Products;
using Abc.Domain.Products.Price;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Products.Price {

    [TestClass]
    public class AppliedPriceTests : AbstractTests<AppliedPrice, BasePrice> {
        private class testClass : AppliedPrice {
            public testClass(PriceData d = null) : base(d) { }
        }
        protected override AppliedPrice createObject() {
            var d = random<PriceData>();
            d.Kind = rndBool ? PriceKind.Agreed : PriceKind.Arbitrary;
            return new testClass(d);
        }
        [TestMethod] public void ProductIdTest() => isReadOnly(obj.Data.ProductId);
        [TestMethod] public async Task ProductTest() =>
            await testItemAsync<ProductData, IProduct, IProductsRepo>(
                obj.ProductId, () => obj.Product.Data, ProductFactory.Create);
    }
}