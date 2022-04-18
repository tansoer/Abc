using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Data.Parties;
using Abc.Data.Products;
using Abc.Domain.Currencies;
using Abc.Domain.Orders;
using Abc.Domain.Orders.Lines;
using Abc.Domain.Products;
using Abc.Domain.Roles;
using Abc.Facade.Orders;
using Abc.Facade.Parties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Tests.Domain.Orders.Lines {

    [TestClass] public class OrderLineTests :SealedTests<OrderLine, TaxableLine> {
        private OrderLineData data;
        protected override OrderLine createObject() => new (data);
        [TestInitialize] public override void TestInitialize() {
            data = random<OrderLineData>();
            data.OrderLineType = OrderLineType.ProductLine;
            base.TestInitialize();
        }
        [TestMethod] public async Task ChargeLinesTest() {
            await RelatedLinesTest();
            var lines = obj.relatedLines;
            var chargeLines = obj.ChargeLines;
            foreach (var l in lines) {
                if (l is not ChargeLine x) continue;
                var y = chargeLines.FirstOrDefault(z => z.Id == x.Id) ?? new ChargeLine();
                allPropertiesAreEqual(x.Data, y.Data);
            }
        }
        [TestMethod] public async Task ProductTypeTest() 
            => await testItemAsync<ProductTypeData, IProductType, IProductTypesRepo>(obj.productTypeId,
            () => obj.ProductType.Data, ProductTypeFactory.Create);
        [TestMethod] public async Task ProductTest() 
            => await testItemAsync<ProductData, IProduct, IProductsRepo>(obj.productId, 
                ()=>obj.Product.Data, ProductFactory.Create);
        [TestMethod] public void ProductTypeIdTest() => isReadOnly(obj.Data.ProductTypeId, true);
        [TestMethod] public void ProductIdTest() => isReadOnly(obj.Data.ProductId, true);
        [TestMethod] public void NumberOfProductsTest() => isReadOnly((int)obj.Data.NumberOfProducts);
        [TestMethod] public void ExpectedDeliveryTest() => isReadOnly(obj.Data.ExpectedDelivery);
        [TestMethod] public async Task UnitPriceTest() {
            isReadOnly();
            var d = random<CurrencyData>();
            d.Id = obj.currencyId;
            await addAsync<ICurrencyRepo, Currency>(new Currency(d));
            var p = obj.UnitPrice;
            areEqual(obj.amount, p.Amount);
            areEqual(obj.currencyId, p.Currency.Id);
        }

        [TestMethod] public async Task RelatedLinesTest()
            => await testListAsync<IOrderLine, OrderLineData, IOrderLinesRepo>(
                x => {
                    x.OrderLineId = obj.Id;
                    x.OrderId = obj.OrderId;
                    x.OrderLineType = random<bool>() ? x.OrderLineType : OrderLineType.ChargeLine;
                }, OrderLineFactory.Create, true);

        [TestMethod]
        public async Task DeliveryReceiverTest() {
            isReadOnly();

            var orderData = random<OrderData>();
            orderData.Id = obj.OrderId;
            await addAsync<IOrdersRepo, IOrder>(OrderFactory.Create(orderData));

            var lineReceiverData = random<PartySummaryData>();
            lineReceiverData.OrderLineId = obj.Id;
            lineReceiverData.OrderId = orderData.Id;
            lineReceiverData.RoleInOrder = PartyRoleInOrder.OrderLineReceiver;
            await addAsync<IPartySummariesRepo, IPartySummary>(PartySummaryFactory.Create(lineReceiverData));


            areEqual(obj.Order.LineReceiver(obj).Id, obj.DeliveryReceiver.Id);
        }

        [TestMethod] public void CloneTest() {
            var o = obj.Clone;
            allPropertiesAreEqual(o.Data, obj.Data);
        }
    }
}
