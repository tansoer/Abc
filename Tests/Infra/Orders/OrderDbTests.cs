using Abc.Data.Common;
using Abc.Data.Currencies;
using Abc.Data.Orders;
using Abc.Facade.Common;
using Abc.Facade.Orders;
using Abc.Infra;
using Abc.Infra.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Abc.Tests.Infra.Orders {

    [TestClass]
    public class OrderDbTests :DbTests<OrderDb, BaseDb<OrderDb>> {
        private class testClass :OrderDb {
            public testClass(DbContextOptions<OrderDb> o) : base(o) { }
            public ModelBuilder RunOnModelCreating() {
                var set = new ConventionSet();
                var mb = new ModelBuilder(set);
                OnModelCreating(mb);
                return mb;
            }
        }
        protected override OrderDb createObject() {
            options = new DbContextOptionsBuilder<OrderDb>().UseInMemoryDatabase("TestDb").Options;
            return new OrderDb(options);
        }
        [TestMethod]
        public void InitializeTablesTest() {
            OrderDb.InitializeTables(null);
            var o = new testClass(options);
            var builder = o.RunOnModelCreating();
            OrderDb.InitializeTables(builder);
            testEntity<DiscountData>(builder);
            testEntity<DiscountTypeData>(builder);
            testEntity<OrderData>(builder);
            testEntity<OrderEventData>(builder);
            testEntity<OrderLineData>(builder);
            testEntity<ManagerData>(builder);
            testEntity<PaymentData>(builder);
            testEntity<OrderLineItemData>(builder);
            testEntity<ReturnedItemData>(builder);
            testEntity<SalesTaxPolicyData>(builder);
            testEntity<TermsAndConditionsData>(builder);
            testEntity<OrderStatusData>(builder);
            testEntity<InvoiceData>(builder);
            testEntity<ProductDeliveryData>(builder);
            testEntity<DeliveryTypeData>(builder);
            testEntity<OrderStatusData>(builder);
        }
        [TestMethod] public void InvoicesTest() => isNullable<DbSet<InvoiceData>>();
        [TestMethod] public void DiscountsTest() => isNullable<DbSet<DiscountData>>();
        [TestMethod] public void DiscountTypesTest() => isNullable<DbSet<DiscountTypeData>>();
        [TestMethod] public void OrdersTest() => isNullable<DbSet<OrderData>>();
        [TestMethod] public void OrderEventsTest() => isNullable<DbSet<OrderEventData>>();
        [TestMethod] public void OrderLinesTest() => isNullable<DbSet<OrderLineData>>();
        [TestMethod] public void OrderManagersTest() => isNullable<DbSet<ManagerData>>();
        [TestMethod] public void OrderPaymentsTest() => isNullable<DbSet<PaymentData>>();
        [TestMethod] public void OrderLineItemsTest() => isNullable<DbSet<OrderLineItemData>>();
        [TestMethod] public void ReturnedItemsTest() => isNullable<DbSet<ReturnedItemData>>();
        [TestMethod] public void SalesTaxPoliciesTest() => isNullable<DbSet<SalesTaxPolicyData>>();
        [TestMethod] public void TermsAndConditionsTest() => isNullable<DbSet<TermsAndConditionsData>>();
        [TestMethod] public void DeliveryTypesTest() => isNullable<DbSet<DeliveryTypeData>>();
        [TestMethod] public void ProductDeliveriesTest() => isNullable<DbSet<ProductDeliveryData>>();
        [TestMethod] public void OrderStatusesTest() => isNullable<DbSet<OrderStatusData>>();
    }
}