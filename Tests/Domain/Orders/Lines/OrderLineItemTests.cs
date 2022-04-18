using Abc.Data.Orders;
using Abc.Data.Products;
using Abc.Domain.Common;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Products;
using Abc.Facade.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Lines {
    [TestClass]
    public class OrderLineItemTests :AbstractTests<OrderLineItem, Entity<OrderLineItemData>> {
        private class testClass:OrderLineItem { 
            public testClass(): this(null) { }
            public testClass(OrderLineItemData d): base(d) { }
        }
        protected override OrderLineItem createObject()
            => new testClass(random<OrderLineItemData>());
        [TestMethod] public void OrderLineIdTest() => isReadOnly(obj.Data.OrderLineId);
        [TestMethod] public void ProductIdTest() => isReadOnly(obj.Data.ProductId);


        [TestMethod]
        public async Task OrderLineTest()
             => await testItemAsync<OrderLineData, IOrderLine, IOrderLinesRepo>(
                 obj.OrderLineId, () => obj.OrderLine.Data, OrderLineFactory.Create);

        [TestMethod]
        public async Task ProductTest()
            => await testItemAsync<ProductData, IProduct, IProductsRepo>(
                obj.Data?.ProductId, () => obj.Product.Data, ProductFactory.Create);
    }
}
