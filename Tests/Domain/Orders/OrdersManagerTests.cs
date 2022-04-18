using Abc.Data.Orders;
using Abc.Domain.Common;
using Abc.Domain.Orders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using Abc.Aids.Random;
using System.Collections.Generic;
using Abc.Domain.Orders.Discounts;
using static Abc.Tests.Domain.Orders.Discounts.DiscountTypeTests;
using Abc.Facade.Orders;

namespace Abc.Tests.Domain.Orders {
    [TestClass] public class OrdersManagerTests: SealedTests<OrdersManager, Manager<IOrdersRepo, IOrder>> {

        [TestMethod] public async Task OrdersTest() => await testListAsync<IOrder, OrderData, IOrdersRepo>(
                d => d.OrderManagerId = obj.Id, OrderFactory.Create);
        [TestMethod] public async Task TaxPoliciesTest() 
            => await testListAsync<SalesTaxPolicy, SalesTaxPolicyData, ISalesTaxPoliciesRepo>(
                d => d.OrderManagerId = obj.Id, d => new SalesTaxPolicy(d));
        [TestMethod] public async Task DiscountTypesTest() 
            => await testListAsync<IDiscountType, DiscountTypeData, IDiscountTypesRepo>(
                d => d.OrderManagerId = obj.Id, d => new DiscountType(d));

        [TestMethod] public void GetDiscountsForOrderNullTest() {
            addMockDeliveryTypes();
            obj.GetDiscounts(null);
            hasNoDiscountsTest();
        }

        private void addMockDeliveryTypes() {
            var list = new List<IDiscountType>();
            var count = random<byte>();
            for (var i = 0; i < count; i++)
                list.Add(new MockDiscountType());
            obj.mockDiscountTypes = list.AsReadOnly();
        }

        private void hasNoDiscountsTest() {
            foreach (MockDiscountType d in obj.mockDiscountTypes)
                areEqual(0, d.Mock.CalledMethods.Count);
        }

        [TestMethod]
        public void GetDiscountsForNotListedPurchaseOrderTest() {
            addMockDeliveryTypes();
            var o = random<OrderData>();
            o.OrderType = OrderType.PurchaseOrder;
            obj.GetDiscounts(OrderFactory.Create(o));
            hasNoDiscountsTest();
        }


        [TestMethod]
        public void GetDiscountsForListedPurchaseOrderTest() {
            addMockDeliveryTypes();
            var o = random<OrderData>();
            o.OrderType = OrderType.PurchaseOrder;
            var order = OrderFactory.Create(o);
            obj.Add(order);
            order = obj.Orders.FirstOrDefault(x => x.Id == order.Id);
            obj.GetDiscounts(order);
            hasNoDiscountsTest();
        }
        [TestMethod]
        public void GetDiscountsForNotListedSalesOrderTest() {
            addMockDeliveryTypes();
            var o = random<OrderData>();
            o.OrderType = OrderType.SalesOrder;
            obj.GetDiscounts(OrderFactory.Create(o));
            hasNoDiscountsTest();
        }
        [TestMethod]
        public void GetDiscountsForListedSalesOrderTest() {
            addMockDeliveryTypes();
            var o = random<OrderData>();
            o.OrderType = OrderType.SalesOrder;
            var order = OrderFactory.Create(o);
            obj.Add(order);
            order = obj.Orders.FirstOrDefault(x => x.Id == order.Id);
            var discounts = obj.GetDiscounts(order);
            foreach (MockDiscountType d in obj.mockDiscountTypes) {
                areEqual(1, d.Mock.CalledMethods.Count);
                areEqual(nameof(d.GetDiscount), d.Mock.CalledMethods[0]);
            }
        }
        [TestMethod] public void GetDiscountsTest() => GetDiscountsForListedSalesOrderTest();

        [TestMethod] public async Task AddOrderTestTest() {
            await DiscountTypesTest();
            var p = OrderFactory.Create(random<OrderData>());
            isTrue(obj.Orders.FirstOrDefault(x => x.Id == p.Id) is null);
            obj.Add(p);
            var actual = obj.Orders.FirstOrDefault(x => x.Id == p.Id);
            allPropertiesAreEqual(p.Data, actual.Data, nameof(p.Data.OrderManagerId));
            areEqual(actual.Data.OrderManagerId, obj.Id);
        }
        [TestMethod] public async Task AddSalesTaxPolicyTest() {
            await TaxPoliciesTest();
            var p = new SalesTaxPolicy(random<SalesTaxPolicyData>());
            isTrue(obj.TaxPolicies.FirstOrDefault(x => x.Id == p.Id) is null);
            obj.Add(p);
            var actual = obj.TaxPolicies.FirstOrDefault(x => x.Id == p.Id);
            allPropertiesAreEqual(p.Data, actual.Data, nameof(p.Data.OrderManagerId));
            areEqual(actual.Data.OrderManagerId, obj.Id);
        }
        [TestMethod] public async Task AddDiscountTypeTest() {
            await DiscountTypesTest();
            var p = new DiscountType(random<DiscountTypeData>());
            isTrue(obj.DiscountTypes.FirstOrDefault(x => x.Id == p.Id) is null);
            obj.Add(p);
            var actual = obj.DiscountTypes.FirstOrDefault(x => x.Id == p.Id);
            allPropertiesAreEqual(p.Data, actual.Data, nameof(p.Data.OrderManagerId));
            areEqual(actual.Data.OrderManagerId, obj.Id);
        }
        [TestMethod] public void AddTest() { }

        [TestMethod] public void RemoveTest() { }
        [TestMethod] public async Task RemoveOrderTest() {
            await OrdersTest();
            var idx = GetRandom.Int32(0, obj.Orders.Count - 1);
            var p = obj.Orders[idx];
            obj.Remove(p);
            isTrue(obj.Orders.FirstOrDefault(x => x.Id == p.Id) is null);
        }
        [TestMethod] public async Task RemoveSalesTaxPolicyTest() {
            await TaxPoliciesTest();
            var idx = GetRandom.Int32(0, obj.TaxPolicies.Count - 1);
            var p = obj.TaxPolicies[idx];
            obj.Remove(p);
            isTrue(obj.TaxPolicies.FirstOrDefault(x => x.Id == p.Id) is null);
        }
        [TestMethod] public async Task RemoveDiscountTypeTest() {
            await DiscountTypesTest();
            var idx = GetRandom.Int32(0, obj.DiscountTypes.Count - 1);
            var p = obj.DiscountTypes[idx] as DiscountType;
            obj.Remove(p);
            isTrue(obj.DiscountTypes.FirstOrDefault(x => x.Id == p.Id) is null);
        }
        [TestMethod]
        public async Task IsOrderTest() {
            await OrdersTest();
            var idx = GetRandom.Int32(0, obj.Orders.Count - 1);
            var o = obj.Orders[idx];
            isTrue(obj.isOrder(o));
            o = OrderFactory.Create(random<OrderData>());
            isFalse(obj.isOrder(o));
        }
        [TestMethod] public async Task IsSalesTaxPolicyTest() {
            await TaxPoliciesTest();
            var idx = GetRandom.Int32(0, obj.TaxPolicies.Count - 1);
            var o = obj.TaxPolicies[idx];
            isTrue(obj.isSalesTaxPolicy(o));
            o = new SalesTaxPolicy(random<SalesTaxPolicyData>());
            isFalse(obj.isSalesTaxPolicy(o));
        }
        [TestMethod] public async Task IsDiscountTypeTest() {
            await DiscountTypesTest();
            var idx = GetRandom.Int32(0, obj.DiscountTypes.Count - 1);
            var o = obj.DiscountTypes[idx] as DiscountType;
            isTrue(obj.isDiscountType(o));
            o = new DiscountType(random<DiscountTypeData>());
            isFalse(obj.isDiscountType(o));
        }
        [TestMethod] public void NewOrderTest() {
            var p = OrderFactory.Create(random<OrderData>());
            var actual = obj.newOrder(p);
            allPropertiesAreEqual(p.Data, actual.Data, nameof(p.Data.OrderManagerId));
            areEqual(actual.Data.OrderManagerId, obj.Id);
        }
        [TestMethod] public void NewTaxPolicyTest() {
            var p = new SalesTaxPolicy(random<SalesTaxPolicyData>());
            var actual = obj.newTaxPolicy(p);
            allPropertiesAreEqual(p.Data, actual.Data, nameof(p.Data.OrderManagerId));
            areEqual(actual.Data.OrderManagerId, obj.Id);
        }
        [TestMethod] public void NewDiscountTypeTest() {
            var p = new DiscountType(random<DiscountTypeData>());
            var actual = obj.newDiscountType(p);
            allPropertiesAreEqual(p.Data, actual.Data, nameof(p.Data.OrderManagerId));
            areEqual(actual.Data.OrderManagerId, obj.Id);
        }
        [TestMethod] public async Task IsListedTaxPolicyTest() {
            await TaxPoliciesTest();
            var idx = GetRandom.Int32(0, obj.TaxPolicies.Count - 1);
            var o = obj.TaxPolicies[idx];
            isTrue(OrdersManager.isListed(obj.TaxPolicies, o.Id));
            o = new SalesTaxPolicy(random<SalesTaxPolicyData>());
            isFalse(OrdersManager.isListed(obj.TaxPolicies, o.Id));
        }
        [TestMethod] public async Task IsListedDiscountTypeTest() {
            await DiscountTypesTest();
            var idx = GetRandom.Int32(0, obj.DiscountTypes.Count - 1);
            var o = obj.DiscountTypes[idx];
            isTrue(OrdersManager.isListed(obj.DiscountTypes, o.Id));
            o = new DiscountType(random<DiscountTypeData>());
            isFalse(OrdersManager.isListed(obj.DiscountTypes, o.Id));
        }
        [TestMethod] public async Task IsListedOrderTest() {
            await OrdersTest();
            var idx = GetRandom.Int32(0, obj.Orders.Count - 1);
            var o = obj.Orders[idx];
            isTrue(obj.isListed(o));
            o = OrderFactory.Create(random<OrderData>());
            isFalse(obj.isListed(o));
        }
    }
}
