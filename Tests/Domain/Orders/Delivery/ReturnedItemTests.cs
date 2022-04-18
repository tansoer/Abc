using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Orders.Delivery;
using Abc.Domain.Products;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Delivery {

    [TestClass]
    public class ReturnedItemTests :SealedTests<ReturnedItem, Entity<ReturnedItemData>> {

        protected override ReturnedItem createObject() => new (random<ReturnedItemData>());
        [TestMethod] public void OrderEventIdTest() => isReadOnly(obj.Data.OrderEventId);
        [TestMethod] public void ProductIdTest() => isReadOnly(obj.Data.ProductId);

        [TestMethod]
        public async Task ProductTest()
            => await testItemAsync<ProductData, IProduct, IProductsRepo>(
                obj.Data?.ProductId, () => obj.Product.Data, ProductFactory.Create);
    }
}